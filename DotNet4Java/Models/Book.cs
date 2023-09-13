namespace Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int? Year { get; set; }
        public string Genre { get; set; }

        public EventHandler<BookEventArgs> BookNotification;


        public Book()
        {

        }

        public Book(string id, string title, Author author, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
        }

        public IEnumerable<Book> GetBooks()
        {
            for (int i = 0; i < 10; i++)
            {
                var authur = new Author("First name " + i, "Last name " + i);
                yield return new Book(Id = $"{i}", Title = $"Title {i}", Author = authur, Genre = "drama");
            }
        }

        public override string ToString()
        {
            return $"Book: Id: {Id}, Title: {Title}, Author: {Author.FirstName}";
        }

        public void buyBook(IEnumerable<Book> books)
        {
            Console.WriteLine("Buying books, please wait ....");
            BookNotification?.Invoke(this, new BookEventArgs { Message = "Payment is success" });
            Thread.Sleep(5000);
            Console.WriteLine("Books bought. Thank you for your purchase!");

        }
    }
}