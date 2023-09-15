using Bookstore.Models;
using System.Text.Json;

namespace Bookstore.Repository
{
    public class AuthorsRepository
    {
        public static string authorsFileName = "authors.txt";
        public static List<Author> _authors = new List<Author>();

        public IEnumerable<Author> GetAuthors()
        {
            var json = File.ReadAllText(authorsFileName);

            _authors = JsonSerializer.Deserialize<List<Author>>(json);

            return _authors;
        }

        public Author GetAuthor(Guid id)
        {
            return _authors.First(author => author.Id == id);
        }

        public void AddAuthor(Author author)
        {
            _authors.Add(author);

            var json = JsonSerializer.Serialize(_authors);

            File.WriteAllText(authorsFileName, json);
        }

        public void RemoveAuthor(Guid id)
        {
            var author = _authors.First(author => author.Id.Equals(id));
            _authors.Remove(author);

            var json = JsonSerializer.Serialize(_authors);

            File.WriteAllText(authorsFileName, json);
        }

    }
}