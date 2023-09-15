using Models;

namespace Data
{
    public class BookRepository
    {
        public async IAsyncEnumerable<Book> GetBooks()
        {
            await foreach (var line in GetLines())
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
        public async IAsyncEnumerable<string> GetLines()
        {  
            await foreach (var line in File.ReadLinesAsync("books.txt"))
            {
                yield return line;
            }
        }
    }
}
