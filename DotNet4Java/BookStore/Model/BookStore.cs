using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Model
{

    internal record BookStore(string Name)
    {
        public HashSet<Book> Books { get; private set; } = new HashSet<Book>();

        public bool AddBook(Book book)
        {
            return Books.Add(book);
        }
    }
    internal record Author(string FirstName, string LastName, string Description)
    {
        public HashSet<BookEdition> BookEditions { get; private set; } = new HashSet<BookEdition>();

        public bool AddBookEdition(BookEdition edition)
        {
            return BookEditions.Add(edition);
        }

    }

    internal record BookEdition(HashSet<Author> authors, string Title, long ISBN) {}
    internal record Book(BookEdition Edition, int Price, BookStore BookStore) {}
}
