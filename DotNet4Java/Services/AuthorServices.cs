using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthorServices
    {
        private readonly AuthorApiRepository _authorsRepository;
        public AuthorServices(AuthorApiRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }
        public List<Author> GetAuthors()
        {
            return _authorsRepository.GetAuthors().ToList();
        }
        public Author GetAuthorById(Guid id)
        {
            return _authorsRepository.GetAuthor(id);
        }
        public void AddAuthor(Author author)
        {
            _authorsRepository.AddAuthor(author);
        }
        public void RemoveAuthor(Guid id)
        {
            _authorsRepository.RemoveAuthor(id);
        }
    }
}
