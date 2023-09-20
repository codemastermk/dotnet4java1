using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using RequestModels;
using Services;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController()
        {
            _shoppingCartService = new ShoppingCartService(new ShoppingCartRepository(), new BookApiRepository());
        }

        [HttpGet]
        public IEnumerable<Book> GetBooksFromCart()
        {
            return _shoppingCartService.GetBooksFromCart();
        }

        [HttpGet]
        [Route("buy-books")]

        public void BuyBooks()
        {
            _shoppingCartService.BuyBooks();
        }

        [HttpPost]
        public IActionResult AddBookInCart(ShoppingCartRequest shoppingCartRequest)
        {
            _shoppingCartService.AddBookInCart(shoppingCartRequest.Id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoveBookFromCart(Guid guid)
        {
            _shoppingCartService.RemoveBookFromCart(guid);
            return Ok();
        }


    }
}
