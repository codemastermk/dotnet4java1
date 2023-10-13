
using Bookstore.Models;
using Bookstore.Repository;

namespace Bookstore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<List<Data.Models.Book>> GetBooks()
        {
            return await _bookRepository.GetAllAsync(); 
        }
    }
}
