using ConsoleTest.Extensions;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace ConsoleTest
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("New branch test 4");

            StringBuilder sb = new StringBuilder();

            int counter = 2;
            bool logicOperator = true;

            sb.Append("This is ")
            .Append(counter)
            .Append(" try.")
            .AppendLine()
            .AppendLine("Hello world!")
            .Replace("Hello", "Hi");

            Console.WriteLine($"This is sample with variables: {counter}, another variable: {logicOperator}!");

            Console.WriteLine(sb);


            ICustomLogger consoleLogger = new ConsoleLogger();
            consoleLogger.LogToConsole("I want this logged.", "ERROR");
            consoleLogger.LogInformation("I want this logged!");

            var book = new Book
            {
                Id = 1,
                Name = "New book",
                Description = "This book is wonderful!",
            };

            book.IsAvailable("asda", out var id, out var description, out var user);

            book.IsAvailableAgain("sgfdg", out id, out description, out user);

            Console.WriteLine($"Book with Id: {id}, Description: {description} is used by {user}");

            string number = "55asdasd";

            if (int.TryParse(number, out var result))
            {
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Result {result} cannot be parsed!");
            }

            ParametersExample();

            int[] myArray = new[] { 1, 2, 15, 166 };
            ref var myNumber = ref GetNumberFromArray(myArray, 2);

            myNumber = 22;

            Console.WriteLine(myNumber);

            Console.ReadLine();
        }

        public static ref int GetNumberFromArray(int[] numbers, int index)
        {
            return ref numbers[index];
        }

        public static void ParametersExample()
        {
            var book = new Book
            {
                Id = 1,
                Name = "Example book",
                Description = "This book is not that wonderful!",
            };

            var author = new Author
            {
                Name = "Author",
                BooksWritten = 19
            };

            AppendBook(ref book);
            Console.WriteLine(book);

            AppendAuthor(ref author);
            Console.WriteLine(author);

            void AppendBook(ref Book book)
            {
                book.Description += " Appended description.";

                book = new Book
                {
                    Id = 2,
                    Name = "Overide book name",
                    Description = "My new description."
                };
            }

            void AppendAuthor(ref Author author)
            {
                author.Name += " Surname";
                author.BooksWritten += 1;
            }
        }
    }
}