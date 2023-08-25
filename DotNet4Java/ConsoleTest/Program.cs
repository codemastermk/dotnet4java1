using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("New branch test 4");

            List<Author> authorList = new List<Author>();

            var author1 = new Author { Id = 1, FirstName = "First", LastName = "First Author" };
            var author2 = new Author { Id = 1, FirstName = "Second", LastName = "Second Author" };

            authorList.Add(author1);
            authorList.Add(author2);

            List<Book> bookList = new List<Book>();
            bookList.Add(new Book { Id = 44, Author = author1, Name = "Some Book 1", Price = 331M });
            bookList.Add(new Book { Id = 63, Author = author1, Name = "Some Book 2", Price = 600M });
            bookList.Add(new Book { Id = 12, Author = author2, Name = "Some Book 3", Price = 400M });

            var expensiveBooks = bookList
                .GetExpensiveBooks()
                .Select(book =>
                new
                {
                    BookName = book.Name,
                    AuthorName = book.Author.LastName + " " + book.Author.FirstName,
                    BookPrice = book.Price
                });
                

            bookList.Add(new Book { Id = 13, Author = author2, Name = "Some Book 4", Price = 1200M });
            bookList.Add(new Book { Id = 16, Author = author2, Name = "Some Book 5", Price = 100M });


            foreach (var book in expensiveBooks)
            {
                Console.WriteLine($"Book: {book.BookName}, Price: {book.BookPrice} From Author: {book.AuthorName}");
            }


            var expensiveBooks2 = bookList.Where(book => book.Price > 500);

            foreach (var book in expensiveBooks2)
            {
                Console.WriteLine($"Book: {book.Name}, Price: {book.Price}");
            }

            ArrayList ar = new ArrayList();

            ar.Add(1);
            ar.Add(2);
            ar.Add("test");
            ar.Add("test 2");

            var extrapolatedList = ar.OfType<string>();

            foreach (var item in extrapolatedList)
            {
                Console.WriteLine(item);
            }


            var author3 = author1;
            if (authorList.Contains(author3, new AuthorComparer()))
            {

                Console.WriteLine($"Author is present {author1.LastName}");
            }

            var skimmedList = bookList.Skip(3).Take(3);

            var element = bookList.Count();
            var sum = bookList.Sum(x => x.Price);
            var average = bookList.Average(book => book.Price);
            var min = bookList.Max(book => book.Price);
            var max = bookList.Min(book => book.Price);


            var sortedList = authorList.ToList();
            var dictionary = bookList.ToDictionary<Book,int, string>(y=> y.Id, x => x.Name);
            var arrayL = bookList.ToArray();

            Console.ReadLine();
        }




    }

    public class AuthorComparer : IEqualityComparer<Author>, IComparer<Author>
    {
        public int Compare(Author? x, Author? y)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Author? x, Author? y)
        {
            if(x == null || y == null)
            {
                return false;
            }

            if(x.Id == y.Id && x.LastName == y.LastName)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Author obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Author Author { get; set; }
    }


    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public static class BookExtensions
    {
        public static IEnumerable<Book> GetExpensiveBooks(this IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                if (book.Price > 399)
                {
                    yield return book;
                }
            }
        }

        public static List<T> Filter<T>(this IEnumerable<T> books, Func<T, bool> func)
        {
            var resultList = new List<T>();
            foreach (var book in books)
            {
                if (func(book))
                {
                    resultList.Add(book);
                }
            }

            return resultList;
        }
    }
}