using BookStore.Model;
using System.Text.Json;

namespace BookStore.Repository
{
    public class BookStoreRepository
    {
        private static string booksName = "books.txt";
        private static List<Book> _books;

        public BookStoreRepository() {
            _books = new List<Book>();
            File.Create(booksName).Dispose();
        }
        public IEnumerable<Book> GetBooks()
        {
            var json = File.ReadAllText(booksName);

            _books = JsonSerializer.Deserialize<List<Book>>(json);

            return _books;
        }

        public Book GetBookById(Guid id)
        {
            return _books.First(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            _books.Add(book);

            var json = JsonSerializer.Serialize(_books);

            File.WriteAllText(booksName, json);
        }

        public void RemoveBookById(Guid id)
        {
            var book = _books.First(book => book.Id.Equals(id));
            _books.Remove(book);

            var json = JsonSerializer.Serialize(_books);

            File.WriteAllText(booksName, json);
        }
    }
}