using Models;

namespace Data
{
    public class AuthorsRepository
    {
        public async IAsyncEnumerable<Author> GetAuthors()
        {
            await foreach (var line in GetLines())
            {
                var items = line.Split(",");

                yield return new Author
                {
                    Id = int.Parse(items[0]),
                    FirstName = items[1],
                    LastName = items[2],
                };
            }
        }

        public async IAsyncEnumerable<string> GetLines()
        {
            yield return "4, Name, Surname";
            yield return "7, Second Name, Second Surname";

            await foreach (var line in File.ReadLinesAsync("authorsData.txt"))
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}");
                await Task.Delay(1000);
                yield return line;

            }

            //string? line;
            //var reader = new StreamReader("authorsData.txt");

            //while ((line = await reader.ReadLineAsync()) != null)
            //{
            //    Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}");
            //    Task.Delay(1000).GetAwaiter().GetResult();
            //    yield return line;
            //}
        }
    }
}