using Data;
using Models;

namespace Services
{
    public class BookService
    {
        private readonly BookApiRepository _booksRepository;
        public BookService(BookApiRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        public List<Book> GetBooks()
        {
            return _booksRepository.GetBooks().ToList();
        }
        public Book GetBookById(Guid id)
        {
            return _booksRepository.GetBook(id);
        }
        public void AddBook(Book book)
        {
            _booksRepository.AddBook(book);
        }
        public void RemoveBook(Guid id)
        {
            _booksRepository.RemoveBook(id);
        }
    }
}