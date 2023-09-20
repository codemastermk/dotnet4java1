using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using RequestModels;


namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorServices _authorService;
        public AuthorController()
        {
            _authorService = new AuthorServices(new AuthorApiRepository());
        }

        [HttpGet]
        public IEnumerable<Author> GetAuthors()
        {
            return _authorService.GetAuthors();
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public Author GetAuthorById([FromRoute] Guid id)
        {
            return _authorService.GetAuthorById(id);
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorRequest authorRequest)
        {
           var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = authorRequest.FirstName,
                LastName = authorRequest.LastName,
            };

            _authorService.AddAuthor(author);
            return CreatedAtRoute("GetAuthorById", new { id = author.Id }, author);
        }

        [HttpDelete]
        public IActionResult RemoveAuthor(Guid guid)
        {
            _authorService.RemoveAuthor(guid);
            return Ok();
        }
    }
}
