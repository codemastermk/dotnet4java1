using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    public class BookApiRepository
    {
        public static string fileName = "books.txt";
        public static List<Book> _books = new List<Book>();

        public IEnumerable<Book> GetBooks()
        {
            var json = File.ReadAllText(fileName);

            _books = JsonSerializer.Deserialize<List<Book>>(json);

            return _books;
        }

        public Book GetBook(Guid id)
        {
            return _books.First(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            _books.Add(book);

            var json = JsonSerializer.Serialize(_books);

            File.WriteAllText(fileName, json);
        }

        public void RemoveBook(Guid id)
        {
            var book = _books.First(book => book.Id.Equals(id));
            _books.Remove(book);

            var json = JsonSerializer.Serialize(_books);

            File.WriteAllText(fileName, json);
        }
    }
}
