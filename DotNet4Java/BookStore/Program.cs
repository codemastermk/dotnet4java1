using BookstoreModel.Model;
using Newtonsoft.Json;
using System;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*BookStore bookStore = new("Literatura");
            Author hpAuthor = new Author("J. K.", " Rowling", "She has performed adult education stuff");
            BookEdition harryPoterChamberOfSecrets = new(new HashSet<Author> { hpAuthor }, "Harry Potter and the Goblet of fire", 1231548684321);
            bookStore.AddBook(new Book(harryPoterChamberOfSecrets, 500));
            string json = JsonConvert.SerializeObject(bookStore);
            System.IO.File.WriteAllText("bookstore.json", json);*/

            string json = System.IO.File.ReadAllText("bookstore.json");
            BookStore bookstore = JsonConvert.DeserializeObject<BookStore>(json);
            // pring all
            Console.WriteLine(bookstore);
            // pring using foreach
            bookstore.Books.ForEach(book => Console.WriteLine(book));
            // pring using linq select
            bookstore.Books.Select(book =>
            {
                Console.WriteLine(book);
                return book;
            }).ToList();


        }

    }
}