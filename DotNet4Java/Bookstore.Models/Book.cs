using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Author> Authors { get; set; } = new List<Author>();
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
