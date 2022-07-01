using Microsoft.EntityFrameworkCore;
using ProjectRepositoryPattern_ModelClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositoryPattern_DataLayer.Context
{
    public class ProjectRepositoryPatternContext : DbContext
    {
        public ProjectRepositoryPatternContext()
        {
        }
        public ProjectRepositoryPatternContext(DbContextOptions<ProjectRepositoryPatternContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
       
    }
}
