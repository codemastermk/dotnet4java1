using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    

    public record BookstoreConfiguration
    {
        public static string BookstoreConfig = "Bookstore";

        public int MaximumBooks { get; set; }
        public int MinimumValueForShipping { get; set; }

        public BookConfiguration BookConfiguration { get; set; } = new BookConfiguration();
    }

    public struct BookConfiguration
    {
        public BookConfiguration()
        {
            
        }
        public int MaxNumberOfAuthors { get; set; } = 20;
    }
}
