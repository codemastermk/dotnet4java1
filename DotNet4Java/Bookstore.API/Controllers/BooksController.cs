using Bookstore.Models;
using Bookstore.RequestModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repository;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public BooksController()
        {
            _bookService = new BookService();
            _authorService = new AuthorService(new AuthorsRepository());
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return _bookService.GetBooks();
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute] Guid id)
        {
            return Ok(_bookService.GetBookById(id));
        }

        [HttpPost]
        public IActionResult AddBook(BookRequest bookRequest)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookRequest.Title,
                Description = bookRequest.Description,
                ISBN = bookRequest.ISBN,
                Price = bookRequest.Price,
                Authors = bookRequest.Authors.Select(_authorService.GetAuthorById).ToList(),
            };

            _bookService.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpDelete]
        public IActionResult RemoveBook(Guid id)
        {
            _bookService.RemoveBook(id);

            return Ok();
        }
    }
}
