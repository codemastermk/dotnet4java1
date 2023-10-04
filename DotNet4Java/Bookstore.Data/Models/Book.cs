namespace Bookstore.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public List<Store>? Stores { get; set; }
        public List<StoreBook>? StoreBooks { get; set; }
    }
}
