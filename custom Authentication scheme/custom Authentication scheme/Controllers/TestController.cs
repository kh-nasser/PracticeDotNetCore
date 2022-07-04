using custom_Authentication_scheme.Authentication.Data;
using custom_Authentication_scheme.AuthHandlers.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace custom_Authentication_scheme.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly TestModel[] testModel;

        public TestController(IHostEnvironment env)
        {
            var text = System.IO.File.ReadAllText(
                Path.Combine(env.ContentRootPath, @"data/test.json"));

            testModel = JsonConvert.DeserializeObject<TestModel[]>(text);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] TokenModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
      .SelectMany(v => v.Errors)
      .Select(e => e.ErrorMessage));

                return UnprocessableEntity(ModelState);
            }

            return Ok(new TokenBuilder().Encode(model));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = AuthSchemeConstants.MyAuthScheme)]
        public TestModel[] Get()
        {
            return testModel;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = AuthSchemeConstants.MyAuthScheme)]
        public TestModel Get(int id)
        {
            return testModel.Where(x =>
                x.Id == id).FirstOrDefault();
        }

        public class TestModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Moniker { get; set; }
            public string[] Techniques { get; set; }
        }
        //private readonly ILogger<TestController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}