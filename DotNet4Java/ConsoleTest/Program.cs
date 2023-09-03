using Data;

namespace ConsoleTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("New branch test 4");


            var collection = Enumerable.Empty<string>();

            var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            var repo = new AuthorsRepository();

            await foreach (var author in repo.GetAuthors())
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }

            Console.ReadLine();
        }
    }
}