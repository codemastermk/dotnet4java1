namespace Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
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