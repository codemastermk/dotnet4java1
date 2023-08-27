using ConsoleTest.Model;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookStore bookStore = new("Literatura");
            Author hpAuthor = new Author("J. K.", " Rowling", "She has performed adult education stuff");
            BookEdition harryPoterChamberOfSecrets = new(new HashSet<Author> { hpAuthor }, "Harry Potter and the Goblet of fire", 1231548684321);
            bookStore.AddBook(new Book(harryPoterChamberOfSecrets, 500, bookStore));
            foreach (Book book in bookStore.Books)
            {
                Console.WriteLine(book);
            }
            bookStore.Books.ToList().ForEach(book => Console.WriteLine(book));
            Dictionary<int, string> dic = new Dictionary<int, string>();
        }

    }
}