using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Model
{
    internal class BookStore
    {
        public HashSet<Book> Books { get; private set; }
        public string Name { get; private set; }

        public bool AddBook(Book book)
        {
            return Books.Add(book);
        }
        public BookStore(string name) 
        {
            Name = name;
            Books = new HashSet<Book>();
        }
    }
}
