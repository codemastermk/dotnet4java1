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
            return BookStore.Books.Find(book => book.Title == title);
        }

        

        public List<string> GetTitles()
        {
            return BookStore.Books.Select(book => book.Title).ToList();
        }

        public Book SellBook(string title)
        {
            Console.WriteLine($"Selling book {title}.");
            Book book = BookStore.Books.Find(x => x.Title == title);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            else
            {
                BookStore.Books.Remove(book);
                return book;
            }
        }

        public void PrintBooks()
        {
            BookStore.Books.ForEach(book => { Console.WriteLine($"{book} "); });
        }
    }
}
