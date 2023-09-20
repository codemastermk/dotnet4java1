using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    public class AuthorApiRepository
    {
        public static string fileName = "authors.txt";
        public static List<Author> _authors = new List<Author>();
        public IEnumerable<Author> GetAuthors()
        {
            var json = File.ReadAllText(fileName);

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

            File.WriteAllText(fileName, json);
        }

        public void RemoveAuthor(Guid id)
        {
            var author = _authors.First(author => author.Id.Equals(id));
            _authors.Remove(author);

            var json = JsonSerializer.Serialize(_authors);

            File.WriteAllText(fileName, json);
        }
    }
}
