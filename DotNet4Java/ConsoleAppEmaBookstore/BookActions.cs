using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEmaBookstore
{
    internal class BookActions
    {
        public EventHandler<BookEventArgs> BookNotification;
        public void BuyBook(IEnumerable<Book> books)
        {
            Console.WriteLine("Buying books, please wait ....");
            Thread.Sleep(5000);
            BookNotification?.Invoke(this, new BookEventArgs { Message = "Payment is success" });
            Console.WriteLine("Books bought. Thank you for your purchase!");
        }
    }
}
