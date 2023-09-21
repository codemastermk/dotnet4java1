namespace RequestModels
{
    public class BookRequest
    {
        public String AuthorName { get; set; } = string.Empty;
        public String Title { get; set; } = string.Empty;
        public String Desctiption { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

    }
}