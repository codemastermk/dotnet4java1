using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using RequestModels;
using Services;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController()
        {
            _bookService = new BookService(new BookApiRepository());
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return _bookService.GetBooks();
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public Book GetBookById([FromRoute] Guid id)
        {
            return _bookService.GetBookById(id);
        }

        [HttpPost]
        public IActionResult AddBook(BookRequest bookRequest)
        {
            var author = new Author
            {
                FirstName = bookRequest.FirstName,
                LastName = bookRequest.LastName,
            };
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookRequest.Title,
                Author = author,
                Genre = bookRequest.Genre,
            };

            _bookService.AddBook(book);
            return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
        }

        [HttpDelete]
        public IActionResult RemoveBook(Guid guid)
        {
            _bookService.RemoveBook(guid);
            return Ok();
        }
    }
}
