using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationExample;

namespace WebRepository.Tests
{
    [TestClass]
    public class BookRepositoryTest
    {
        [TestMethod]
        public void FetchBookTest()
        {
            //arrange
            IQueryable<Book> books = new List<Book>() {
                new Book() {
                    Title ="Hamlet",
                    Author = "William Shakespeare"
                },
                new Book() {
                    Title ="The Girl You Left Behind",
                    Author = "Jojo Moyes"
                },
            }
            .AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockContext = new Mock<BookStoreContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);
            //act
            var repository = new BookRepository(mockContext.Object);
            var actual = repository.FetchBooks();
            
            //asserts
            Assert.AreEqual(2, actual.Count);   
            Assert.AreEqual("Hamlet", actual.First().Title);   
        }

        [TestMethod]
        public void CreateBookTest() {
            //arrange
            var mockSet = new Mock<DbSet<Book>>();
            var mockContext = new Mock<BookStoreContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);
            //act
            var repository = new BookRepository(mockContext.Object);
            repository.AddBook( "TestBookName", "TestName" );
            //asserts
            mockSet.Verify(m=>m.Add(It.IsAny<Book>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges());
        }
    }
}
