using Bookstore.Models;
using Bookstore.Repository;

namespace Bookstore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Data.Models.Author?> GetAuthorById(int id)
        {
            return await _unitOfWork.AuthorsRepository.GetByIdAsync(id);

        }
    }
}