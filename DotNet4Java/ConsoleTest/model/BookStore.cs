using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    internal class BookStore
    {
        public BookStore() {
            this.books = new List<Book>();
        }
        private List<Book> books;
        public List<Book> Books { get { return books; } }
    }
}
