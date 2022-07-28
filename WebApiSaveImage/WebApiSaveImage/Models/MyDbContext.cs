using Microsoft.EntityFrameworkCore;

namespace WebApiSaveImage.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
