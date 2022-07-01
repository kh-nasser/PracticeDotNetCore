using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using ProjectRepositoryPattern_DataLayer.Context;
using ProjectRepositoryPattern_DataLayer.Repository;
using ProjectRepositoryPattern_DataLayer.Services;
using ProjectRepositoryPattern_ModelClass.Models;

namespace ProjectRepositoryPattern_MyTestSite.Controllers
{
    public class PeopleController : Controller
    {
        private ProjectRepositoryPatternContext db;
        private IPersonRepository personRepository;

        public PeopleController(ProjectRepositoryPatternContext db)
        {
            this.db = db;
            personRepository = new PersonRepository(db);
        }

        // GET: People
        public ActionResult Index()
        {
            return View(personRepository.GetAllPerson());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Person person = personRepository.GetPersonById(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PersonID,Name,Family,WebSite")] Person person)
        {
            if (ModelState.IsValid)
            {
                personRepository.InsertPerson(person);
                personRepository.Save();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Person person = personRepository.GetPersonById(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("PersonID,Name,Family,WebSite")] Person person)
        {
            if (ModelState.IsValid)
            {
                personRepository.UpdatePerson(person);
                personRepository.Save();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Person person = personRepository.GetPersonById(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            personRepository.DeletePerson(id);
            personRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
