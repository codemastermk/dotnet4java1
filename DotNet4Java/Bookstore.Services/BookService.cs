
using Bookstore.Models;
using Bookstore.Repository;

namespace Bookstore.Services
{
    public class BookService
    {
        public static Dictionary<Guid, Book> _books = CreateBooks();

        public IEnumerable<Book> GetBooks()
        {
            return _books.Values;
        }

        public Book GetBookById(Guid id)
        {
            if (_books.TryGetValue(id, out var book))
            {
                return book;
            }

            return new Book();
        }

        public void AddBook(Book book)
        {
            _books.Add(book.Id, book);
        }

        public void RemoveBook(Guid id)
        {
            _books.Remove(id);
        }


        public static Dictionary<Guid, Book> CreateBooks()
        {
            var bookDictionary = new Dictionary<Guid, Book>();
            var authorService = new AuthorService(new AuthorsRepository());
            var authors = authorService.GetAuthors();

            var guid = Guid.NewGuid();
            bookDictionary.Add(guid, new Book
            {
                Id = guid,
                Title = "Harry Potter",
                Description = "Harry Potter Description",
                ISBN = "1234567890123",
                Price = 100M,
                Authors = new List<Author> { authors.First() },
            });

            guid = Guid.NewGuid();
            bookDictionary.Add(guid, new Book
            {
                Id = guid,
                Title = "Hobbit",
                Description = "Hobbit Description",
                ISBN = "0987654321234",
                Price = 140M,
                Authors = new List<Author>(authors.Skip(1).Take(1)),
            });

            guid = Guid.NewGuid();
            bookDictionary.Add(guid, new Book
            {
                Id = guid,
                Title = "Ice and fire",
                Description = "Ice and fire Description",
                ISBN = "7890123456789",
                Price = 10M,
                Authors = new List<Author>(authors.Skip(2).Take(1)),
            });

            return bookDictionary;
        }
    }
}
