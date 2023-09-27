using Bookstore.Models;
using Bookstore.RequestModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repository;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}[controller]")]
    
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public BooksController()
        {
            _bookService = new BookService();
            _authorService = new AuthorService(new AuthorsRepository());
        }

        /// <summary>
        /// Gets All Books
        /// </summary>
        /// <returns>List of Books</returns>
        /// <response code="200">Books</response>
        /// <response code="400">Error</response>
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
               return Ok(_bookService.GetBooks());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute] Guid id)
        {
            return Ok(_bookService.GetBookById(id));
        }

        /// <summary>
        /// Create Book
        /// </summary>
        /// <returns>Created Books</returns>
        /// <remarks>
        /// Sample Request: 
        ///     POST /AddBook
        ///     {
        ///         "title": "some title",
        ///         "description": "some description"
        ///     }
        /// </remarks>
        /// <response code="201">Book</response>
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
