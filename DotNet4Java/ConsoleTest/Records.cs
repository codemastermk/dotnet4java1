using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleTest
{
    internal record ClassAsRecord(int Id, string Name, string Description)
    {
        public int Quantity { get; init; } = 5;
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
