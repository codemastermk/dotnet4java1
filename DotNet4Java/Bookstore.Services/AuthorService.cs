using Bookstore.Models;
using Bookstore.Repository;

namespace Bookstore.Services
{
    public class AuthorService
    {
        //private static Dictionary<Guid, Author> _authors = CreateStaticAuthors();
        private readonly AuthorsRepository _authorsRepository;

        public AuthorService(AuthorsRepository authorsRepository)
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


        //private static Dictionary<Guid, Author> CreateStaticAuthors()
        //{
        //    var authorsDictionary = new Dictionary<Guid, Author>();

        //    var guid = Guid.NewGuid();
        //    authorsDictionary.Add(guid, new Author
        //    {
        //        Id = guid,
        //        Name = "Joanne Rowling",
        //    });

        //    guid = Guid.NewGuid();
        //    authorsDictionary.Add(guid, new Author
        //    {
        //        Id = guid,
        //        Name = "John Tolkien",
        //    });

        //    guid = Guid.NewGuid();
        //    authorsDictionary.Add(guid, new Author
        //    {
        //        Id = guid,
        //        Name = "George Martin",
        //    });

        //    return authorsDictionary;
        //}

    }
}