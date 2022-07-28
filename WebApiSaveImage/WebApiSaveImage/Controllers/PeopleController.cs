using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebApiSaveImage.Models;

namespace WebApiSaveImage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public PeopleController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return dbContext.People;
        }

        [HttpPost]
        [Produces(typeof(ActionResult))]
        public async Task<ActionResult> UploadAvatar([FromForm] UpdateImageRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException();
            }
            if (Request == null || Request.Form == null || Request.Form.Files == null || Request.Form.Files.Count != 1)
            {
                throw new HttpRequestException();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult Post()
        {
            var data = HttpContext.Request.Form;
            var dic = HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            Person modelPerson = new Person();
            foreach (var kvp in data.Keys)
            {
                PropertyInfo propertyInfo = modelPerson.GetType().GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(modelPerson, dic[kvp], null);
                }
            }

            if (data.Files.Count > 0)
            {
                IFormFile img = data.Files[0];
                modelPerson.ImageName = img.FileName;
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", modelPerson.ImageName);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    img.CopyTo(fs);
                }
            }

            dbContext.Add(modelPerson);
            dbContext.SaveChanges();

            return Ok(modelPerson);
        }


        [Produces(typeof(UpdateImageRequestDto))]
        [HttpPost]
        public async Task<IActionResult> SubmitAvatarForm()
        {

            if (!ModelState.IsValid)
            {
                throw new HttpRequestException();
            }
            if (Request == null || Request.Form == null || Request.Form.Files == null || Request.Form.Files.Count != 1)
            {
                throw new HttpRequestException();
            }

            var file = Request.Form.Files[0];
            return Ok(file);
        }

    }
}
