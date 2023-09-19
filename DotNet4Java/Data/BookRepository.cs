using Models;

namespace Data
{
    public class BookRepository
    {
        public async IAsyncEnumerable<Book> GetBooksAsync()
        {
            await foreach (var line in GetLinesAsync())
            {
                var items = line.Split(",");
                var author = new Author(items[2], items[3]);
                yield return new Book
                {
                    Id = items[0],
                    Title = items[1],
                    Author = author,
                    Genre = items[4],
                };
            }
        }
        public async IAsyncEnumerable<string> GetLinesAsync()
        {  
            await foreach (var line in File.ReadLinesAsync("books.txt"))
            {
                yield return line;
            }
        }
    }
}
