using ConsoleTest.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.service.impl
{
    internal class BookStoreServiceImpl : IBookStoreService
    {
        private BookStore BookStore { get; set; }

        

        public BookStoreServiceImpl(BookStore bookStore)
        {
            BookStore = bookStore;
        }

        public Book GetBook(string title)
        {
            return BookStore.Books.Where(book => book.Title == title).FirstOrDefault();
        }

        

        public IEnumerable<string> GetTitles()
        {
            return BookStore.Books.Select(book => book.Title).ToList();
        }

        public void SellBook(string title)
        {
            Console.WriteLine($"Selling book {title}.");
            Book book = BookStore.Books.Where(book => book.Title == title).FirstOrDefault();
            if (book == null)
            {
                Console.WriteLine("Book not found");
            }
            else
            {
                BookStore.Books.Remove(book);
            }
        }

        public void PrintBooks()
        {
            BookStore.Books.ToList().ForEach(book => { Console.WriteLine($"{book} "); });
        }
    }
}
