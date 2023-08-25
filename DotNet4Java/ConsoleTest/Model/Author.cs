using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Model
{
    internal class Author
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Description { get; private set; }

        public HashSet<BookEdition> BookEditions { get; private set; }

        public bool AddBookEdition(BookEdition edition)
        {
            return BookEditions.Add(edition);
        }

        public Author(string firstName, string lastName, string description) 
        {
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            BookEditions = new HashSet<BookEdition>();
        }
    }

    internal record BookEdition(HashSet<Author> authors, string Title, long ISBN) {}
    internal record Book(BookEdition edition, int price, BookStore bookStore) { }
}
