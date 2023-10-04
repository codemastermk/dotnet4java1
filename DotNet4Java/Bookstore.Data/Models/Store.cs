namespace Bookstore.Data.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Book>? Books { get; set; }
        public List<StoreBook>? StoreBooks { get; set; }
    }
}
