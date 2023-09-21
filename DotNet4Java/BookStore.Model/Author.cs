using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Author
    {
        public string Name { get; }

        public Author(string name)
        {
            Name = name;
        }
    }
}
