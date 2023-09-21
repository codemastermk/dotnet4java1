using BookStore.Model;
using BookStore.Repository;

namespace BookStore.Service
{
    public class BookStoreService : IBookStoreService
    {
        private BookStoreRepository _bookStoreRepository;



        public BookStoreService(BookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }

        public Book GetBook(String title)
        {
            return _bookStoreRepository.GetBooks().FirstOrDefault(book => book.Title == title);
        }

        public IEnumerable<string> GetTitles()
        {
            return _bookStoreRepository.GetBooks().Select(book => book.Title).ToList();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _bookStoreRepository.GetBooks();
        }

        public void AddBook(Book book)
        {
            _bookStoreRepository.AddBook(book);
        }

        public Book GetBookById(Guid Id) {
            return _bookStoreRepository.GetBookById(Id);
        }

        public void RemoveBookById(Guid Id)
        {
            _bookStoreRepository.RemoveBookById(Id);
        }
    }
}