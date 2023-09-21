using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.Service
{
    public interface IBookStoreService
    {
        void AddBook(Book book);
        IEnumerable<Book> GetBooks();

        IEnumerable<String> GetTitles();
        Book GetBook(String Title);

        Book GetBookById(Guid Id);

        void RemoveBookById(Guid Guid);
    }
}
