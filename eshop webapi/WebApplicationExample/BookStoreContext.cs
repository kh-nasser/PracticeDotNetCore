using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationExample
{
    public class BookStoreContext :DbContext
    {
        public virtual DbSet<Book> Books { get; set; }
        public BookStoreContext()
        {

        } 
        
        public BookStoreContext(DbContextOptions<BookStoreContext> options): base(options)
        {

        }
    }

    public class Book {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title{ get; set; }
        [Required]
        public string Author{ get; set; }
        public DateTime DatePublished{ get; set; }

        public Book()
        {
            DatePublished = new DateTime();
        }
    }

}
