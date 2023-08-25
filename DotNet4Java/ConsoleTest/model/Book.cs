using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    internal class Book
    {

        public Author Author { get; private set; }
        public BookStore BookStore { get; private set; }
        public decimal Price { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Book(Author author, BookStore bookStore, decimal price, string title, string description)
        {
            Author = author;
            BookStore = bookStore;
            Price = price;
            Title = title;
            Description = description;
        }

        public override string? ToString()
        {
            return String.Format($"Title: {Title} \n" +
                $"Author: {Author.Name} \n" +
                $"Price: {Price} \n" +
                $"Description: {Description} \n\n");
        }
    }
}
