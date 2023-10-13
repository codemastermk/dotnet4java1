using Bookstore.Models;
using Bookstore.Repository;

namespace Bookstore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorsRepository _authorsRepository;

        private readonly IBookRepository _bookRepository;
        public AuthorService(IAuthorsRepository authorsRepository, IBookRepository bookRepository)
        {
            _authorsRepository = authorsRepository;
            _bookRepository = bookRepository;
        }
        public async Task<Data.Models.Author?> GetAuthorById(int id)
        {
            return await _authorsRepository.GetByIdAsync(id);
        }
    }
}