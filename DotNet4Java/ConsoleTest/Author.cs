namespace ConsoleTest
{
    internal partial class Program
    {
        public record struct Author
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int BooksWritten { get; set; }
        }
    }
}