using Microsoft.EntityFrameworkCore;
using ProjectRepositoryPattern_DataLayer.Context;
using ProjectRepositoryPattern_DataLayer.Repository;
using ProjectRepositoryPattern_ModelClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositoryPattern_DataLayer.Services
{
    public class PersonRepository : IPersonRepository
    {
        private ProjectRepositoryPatternContext db;

        public PersonRepository(ProjectRepositoryPatternContext context)
        {
            db = context;
        }

        public void DeletePerson(int personId)
        {
            var person = GetPersonById(personId);
            DeletePerson(person);
        }

        public void DeletePerson(Person person)
        {
            db.Entry(person).State = EntityState.Deleted;
        }

        public List<Person> GetAllPerson()
        {
            
            return db.Persons.ToList();
        }

        public Person GetPersonById(int personId)
        {
            return db.Persons.Find(personId);
        }

        public void InsertPerson(Person person)
        {
            db.Persons.Add(person);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            db.Entry(person).State = EntityState.Modified;
        }
    }
}
