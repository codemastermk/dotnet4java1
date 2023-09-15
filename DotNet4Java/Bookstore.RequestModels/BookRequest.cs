using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.RequestModels
{
    public class BookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> Authors { get; set; } = new List<Guid>();
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
