using Bookstore.Models;
using Bookstore.RequestModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repository;
using Bookstore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}[controller]")]
    
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        private readonly BookstoreContext _dbContext;
        public BooksController(BookstoreContext context)
        {
            _dbContext = context;
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

                var query = from b in _dbContext.Books
                            join a in _dbContext.Authors
                            on b.AuthorId equals a.Id
                            select b;
                var result = await query.ToArrayAsync();

                //var id = 1;
                //var result = await _dbContext.Books.FromSqlInterpolated(@$"select * from books where id = {1}").Include(a => a.Author).ToListAsync();

                // explicit
                //var author = _dbContext.Authors.Single(a => a.Id == 1);
                //_dbContext.Entry(author).Collection(b => b.Books).Load();
                
                // eager
                //var result = _dbContext.Authors.Include(b => b.Books).Skip(0).Take(10).ToList();
                
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
            // no tracking
            // var result = await _dbContext.Books.AsNoTracking().ToListAsync();

            // var book = _dbContext.Books.FirstOrDefault();

            // if(book == null)
            // {
            //    return NotFound();
            // }

            // var result = _dbContext.Books.OrderBy(x => x.Id).AsEnumerable();
            // result = result.Where(x => x.Description.Contains("3"));
            // var result = await _dbContext.CountAuthorsAsync();

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
