
using ConsoleAppEmaBookstore.Services;
using Data;
using Models;

namespace ConsoleAppHomework
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine("Welcome to BOOKSIN bookstore");
            Console.WriteLine("Please choose book(s) you would like to buy in the following format 'ID;ID1;ID2...'!");

            var book = new Book();
            var books = [];
            foreach (var b in books)
            {
                Console.WriteLine(b);
            }
            var readLine = Console.ReadLine();
            if (readLine != null)
            {
                ShowSelectedBooks(books, readLine, out var booksToBuy);
                Console.WriteLine("Books to buy!");

                foreach (var b in booksToBuy)
                {
                    Console.WriteLine(b);
                }

                var sms = new SmsService();
                var email = new MailServvice();
                book.BookNotification += sms.Notify;
                book.BookNotification += email.Notify;
                book.buyBook(booksToBuy);

            }
        }

        public static void ShowSelectedBooks(IEnumerable<Book> books, string readLine, out List<Book> returnBooks)
        {
            returnBooks = new List<Book>();
            var booksIds = ParseReadLine(readLine);
            foreach (var id in booksIds)
            {
                returnBooks.AddRange(books.ToList().Where(x => x.Id == id));
            }
        }


        public static string[] ParseReadLine(string readLine)
        {
            return readLine.Split(';');
        }
    }
}