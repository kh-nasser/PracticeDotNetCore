namespace WebApplicationExample
{
    public class BookRepository
    {
        private BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            this._context = context;
        }

        public void AddBook(string title, string author)
        {
            Book book = new Book()
            {
                Author = author,
                Title = author
            };
            _context.Books.Add(book);
            _context.SaveChanges(); 
        }

        public List<Book> FetchBooks() { 
            return _context.Books.ToList();
        }
    }
}
