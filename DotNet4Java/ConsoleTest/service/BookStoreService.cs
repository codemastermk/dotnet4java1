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
        Book SellBook(string title);
        void PrintBooks();

        List<String> GetTitles();
        Book GetBook(String title);
    }
}
