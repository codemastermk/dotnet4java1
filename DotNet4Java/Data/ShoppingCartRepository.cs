using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    public class ShoppingCartRepository
    {
        public static string fileName = "shoppingCart.txt";
        public static List<Book> _booksToBuy = new List<Book>();

        public IEnumerable<Book> GetBooksFromCart()
        {
            var json = File.ReadAllText(fileName);
            if (json.Any())
            {
                _booksToBuy = JsonSerializer.Deserialize<List<Book>>(json);
            }

            return _booksToBuy;
        }
        public void AddBookInCart(IEnumerable<Book> books)
        {
            _booksToBuy.AddRange(books);

            var json = JsonSerializer.Serialize(_booksToBuy);

            File.WriteAllText(fileName, json);
        }
        public void RemoveBookFromCart(Guid id)
        {
            var book = _booksToBuy.First(book => book.Id.Equals(id));
            _booksToBuy.Remove(book);

            var json = JsonSerializer.Serialize(_booksToBuy);

            File.WriteAllText(fileName, json);
        }
        public void BuyBooks()
        {
            if (_booksToBuy.Any())
            {
                Console.WriteLine("Buying books, please wait ....");
                Thread.Sleep(5000);
                Console.WriteLine("Books bought. Thank you for your purchase!");
            } 
            else
            {
                Console.WriteLine("Shopping cart is empty, please add new books ....");
            }
           
        }
    }
}
