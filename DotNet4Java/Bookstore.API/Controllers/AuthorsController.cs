using Bookstore.Models;
using Bookstore.RequestModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repository;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController()
        {
            _authorService = new AuthorService(new AuthorsRepository());
        }

        [HttpGet]
        public IEnumerable<Author> GetAuthors()
        {
            return _authorService.GetAuthors();
        }

        [HttpGet("{id}", Name = "GetSingleAuthor")]
        public IActionResult GetAuthorById([FromRoute] Guid id)
        {
            return NotFound(_authorService.GetAuthorById(id));
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorRequest authorRequest)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = authorRequest.Name,
            };

            _authorService.AddAuthor(author);

            return CreatedAtRoute("GetSingleAuthor", new { id = author.Id }, author);
        }

        [HttpDelete]
        public IActionResult RemoveAuthor(Guid guid)
        {
            _authorService.RemoveAuthor(guid);

            return Ok();
        }
    }
}
