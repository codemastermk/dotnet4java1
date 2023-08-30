using System.Diagnostics.CodeAnalysis;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var author1 = new Author
            {
                Id = 1,
                FirstName = "John",
                LastName = "Tolkien"
            };
            var author2 = new Author
            {
                Id = 2,
                FirstName = "George",
                LastName = "Martin"
            };
            var author3 = new Author
            {
                Id = 3,
                FirstName = "Joanne",
                LastName = "Rowling"
            };

            var book1 = new Book
            {
                Id = 1,
                Name = "H.P book 1",
                Authors = new List<Author> { author1, author2 }
            };

            var book2 = new Book
            {
                Id = 2,
                Name = "H.P book 2",
                Authors = new List<Author> { author1, author3 }
            };

            var book3 = new Book
            {
                Id = 1,
                Name = "H.P book 1",
                Authors = new List<Author> { author1, author3 }
            };

            var books = new List<Book> { book1, book2 };

            var authors = books.SelectMany(b => b.Authors);

            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }

         
            Dictionary<Book, int> counts = new Dictionary<Book, int>(new BookComparer());
            counts.Add(book1, 1);
            counts.Add(book2, 2);
            counts.Add(book3, 3);


            Console.ReadLine();
        }
    }

    public class BookComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book? x, Book? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if(x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id
                && x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase);

        }

        public int GetHashCode([DisallowNull] Book obj)
        {
            return obj.Id.GetHashCode() ^ obj.Name.GetHashCode();
        }
    }


    public class Book
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Author> Authors { get; set; }

        //public override bool Equals(object? obj)
        //{
        //    if(obj is Book book)
        //    {
        //        if(book.Id == Id)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return Id.GetHashCode();
        //}
    }

    public record Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}