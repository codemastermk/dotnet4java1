using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Book
    {
        public Guid Id { get; private set; }
        public Author Author { get; private set; }
        public decimal Price { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Book(Author author, decimal price, string title, string description)
        {
            Id = Guid.NewGuid();
            Author = author;
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
