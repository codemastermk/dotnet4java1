namespace Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Genre { get; set; }
        public override string ToString()
        {
            return $"Book: Id: {Id}, Title: {Title}, Author: {Author.FirstName}";
        }
      
    }
}