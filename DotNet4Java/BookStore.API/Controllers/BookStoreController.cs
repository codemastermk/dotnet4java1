using Microsoft.AspNetCore.Mvc;
using BookStore.Service;
using BookStore.Model;
using RequestModels;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookStoreController : ControllerBase
    {

        private readonly ILogger<BookStoreController> _logger;
        private readonly IBookStoreService _bookStoreService;

        public BookStoreController(ILogger<BookStoreController> logger, IBookStoreService bookStoreService)
        {
            _logger = logger;
            _bookStoreService = bookStoreService;
        }

        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return _bookStoreService.GetBooks();
        }

        [HttpPost]
        public IActionResult AddBook(BookRequest bookRequest)
        {
            var book = new Book(
                new Author(bookRequest.AuthorName),
                bookRequest.Price,
                bookRequest.Title,
                bookRequest.Desctiption
            );

            _bookStoreService.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpGet]
        public IActionResult GetBookById(Guid id)
        {
            var book = _bookStoreService.GetBookById(id);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpDelete]
        public IActionResult DeleteBookById(Guid id)
        {
            _bookStoreService.RemoveBookById(id);

            return Ok();
        }
    }
}