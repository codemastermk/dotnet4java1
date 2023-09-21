using ConsoleTest.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.service
{
    internal interface BookService
    {
        Book BuyBook(String title);
        List<Book> GetBooks();
        List<String> GetTitles();
    }
}
