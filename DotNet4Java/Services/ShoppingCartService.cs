using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ShoppingCartService
    {
        private readonly ShoppingCartRepository _shoppingCartRepository;
        private readonly BookApiRepository _bookApiRepository;
        public ShoppingCartService(ShoppingCartRepository shoppingCartRepository,
            BookApiRepository bookApiRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _bookApiRepository = bookApiRepository;
        }
        public List<Book> GetBooksFromCart()
        {
            return _shoppingCartRepository.GetBooksFromCart().ToList();
        }
        public void AddBookInCart(Guid id)
        {
            var book = _bookApiRepository.GetBooks().Where(book => book.Id == id);
            _shoppingCartRepository.AddBookInCart(book);
        }
        public void RemoveBookFromCart(Guid id)
        {
            _shoppingCartRepository.RemoveBookFromCart(id);
        }
        public void BuyBooks()
        {
            _shoppingCartRepository.BuyBooks();
        }
    }
}
