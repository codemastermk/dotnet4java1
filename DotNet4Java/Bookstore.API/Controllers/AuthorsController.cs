using Bookstore.Models;
using Bookstore.RequestModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repository;
using static Bookstore.API.Configuration.DependencyDelegate;
using Bookstore.API.Configuration;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Data;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data.Models;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly ISerialNumber _serialNumber;
        private readonly IComplexSerialNumber _complexSerialNumber;
        private readonly ILogger<AuthorsController> _logger;
        private readonly ISerialNumber _factorySerialNumber;
        private readonly ISerialNumber _resolvedSerialNumber;
        private readonly BookstoreContext _context;

        public AuthorsController(AuthorService authorService, 
            IEnumerable<ISerialNumber> serialNumbers, 
            // [FromKeyedServices("Simple")]ISerialNumber serialNumber8,
            IComplexSerialNumber complexSerialNumber,
            SerialNumberResolver resolver,
            IDependencyFactory dependencyFactory,
            ILogger<AuthorsController> logger,
            BookstoreContext context
            )
        {
            _authorService = authorService;
            this._serialNumber = serialNumbers.Where(x => x.GetType() == typeof(SerialNumber)).First();
            this._complexSerialNumber = complexSerialNumber;
            _logger = logger;
            _factorySerialNumber = dependencyFactory.GetDependency("Original");
            _resolvedSerialNumber = resolver("Original");
            _context = context;
        }

        //[HttpGet]
        //public IEnumerable<Author> GetAuthors()
        //{
        //    return _authorService.GetAuthors();
        //}

        [HttpGet("{id}", Name = "GetSingleAuthor")]
        public IActionResult GetAuthorById([FromRoute] Guid id)
        {
            return NotFound(_authorService.GetAuthorById(id));
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorRequest authorRequest)
        {
            //using var transaction = _context.Database.BeginTransaction();
            try
            {

                var author = new Data.Models.Author { Name = "Author Name" };
                _context.Authors.Add(author);
                //_context.SaveChanges();
                //transaction.CreateSavepoint("treter");

                _context.Books.Add(new Data.Models.Book { Title = "Some Title", Author =  author});
                _context.SaveChanges();
                
                //transaction.Commit();

                return Ok();
            }
            catch(Exception ex)
            {
                //transaction.RollbackToSavepoint("");
                //transaction.Rollback();
                return BadRequest(ex.Message);
            }
                       
            
            //var author = new Author
            //{
            //    Id = Guid.NewGuid(),
            //    Name = authorRequest.Name,
            //};

            //_authorService.AddAuthor(author);

            //return CreatedAtRoute("GetSingleAuthor", new { id = author.Id }, author);
        }

        [HttpDelete]
        public IActionResult RemoveAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            _context.Authors.Remove(author);
            _context.SaveChanges();
            //_authorService.RemoveAuthor(guid);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetSerialNumber()
        {
            _logger.LogCritical("This is a critical value {SomeValue} {SomeOtherValue}", _factorySerialNumber.GetSerialNumber(), _serialNumber.GetSerialNumber());
            List<string> serials = _complexSerialNumber.GetComplexSerialNumber();

            serials.Add(_serialNumber.GetSerialNumber());
            serials.Add(_resolvedSerialNumber.GetSerialNumber());
            serials.Add(_factorySerialNumber.GetSerialNumber());

            return Ok(serials);
        }
    }
}
