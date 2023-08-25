using ConsoleTest.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.service
{
    internal interface IBookStoreService
    {
        void SellBook(string title);
        void PrintBooks();

        IEnumerable<String> GetTitles();
        Book GetBook(String title);
    }
}
