using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreModel.Model
{

    public record BookStore(string Name)
    {
        public List<Book> Books { get; private set; } = new List<Book>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }
    }
    public record Author(string FirstName, string LastName, string Description)
    {
        public HashSet<BookEdition> BookEditions { get; private set; } = new HashSet<BookEdition>();

        public bool AddBookEdition(BookEdition edition)
        {
            return BookEditions.Add(edition);
        }

    }

    public record BookEdition(HashSet<Author> authors, string Title, long ISBN) { }
    public record Book(BookEdition Edition, int Price) { }
}
