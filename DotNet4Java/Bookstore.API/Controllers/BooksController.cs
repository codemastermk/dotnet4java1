using Bookstore.Models;
using Bookstore.RequestModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repository;
using Bookstore.Data;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data.Models;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}[controller]")]

    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                return Ok(await _bookService.GetBooks());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
