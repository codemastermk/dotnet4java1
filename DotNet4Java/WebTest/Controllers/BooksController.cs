using Microsoft.AspNetCore.Mvc;

namespace WebTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BooksAndAuthorsController : ControllerBase
    {
        [HttpGet("MyBook")]
        public string GetBook()
        {
            return "This is book from the controller";
        }
    }
}
