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
    //[Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorsRepository authorsRepository, IAuthorService authorService)
        {
            _authorService = authorService;
            _authorsRepository = authorsRepository;           
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                return Ok(await _authorsRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var result = await _authorService.GetAuthorById(id);
                if(result is null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
