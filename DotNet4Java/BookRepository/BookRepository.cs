namespace Book
{
    public class BookRepository
    {
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var rand = new Random();
            var number = rand.Next(1, 100);

            var books = new List<Book>();
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(10);
                if (i == number)
                {
                    books.Add(new Book
                    {
                        Id = i,
                        Name = $"Book name {i}",
                        ISBN = null,
                    });
                }
                else
                {
                    books.Add(new Book
                    {
                        Id = i,
                        Name = $"Book name {i}",
                        ISBN = $"ISBN of book {i}"
                    });
                }
            }

            return books;
        }

        public int CountBooks(IEnumerable<Book> books)
        {
            return books.Count();
        }

        public string GetBookName(int id)
        {
            if(id < 10)
            {
                return "Book one";
            }
            else
            {
                return "Book two";
            }
        }

        public int AddMethod(int a, int b)
        {
            return a + b;
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
    }
}
