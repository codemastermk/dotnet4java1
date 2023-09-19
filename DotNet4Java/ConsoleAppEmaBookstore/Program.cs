
using ConsoleAppEmaBookstore.Services;
using Data;
using Models;
using System.Collections;

namespace ConsoleAppEmaBookstore
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to BOOKSIN bookstore");
            Console.WriteLine("Please choose book(s) you would like to buy in the following format 'ID;ID1;ID2...'!");

            var repo = new BookRepository();
            var bookActions = new BookActions();
            Dictionary<string, Book> books = new Dictionary<string, Book>();

            await foreach (var b in repo.GetBooksAsync())
            {
                books.Add(b.Id, b);
                Console.WriteLine(b);
            }

            var readLine = Console.ReadLine();
            if (readLine != null)
            {
                ShowSelectedBooks(books, readLine, out var booksToBuy);
                SendNotifications(bookActions);
                bookActions.BuyBook(booksToBuy);
            }
        }
        private static void ShowSelectedBooks(Dictionary<string, Book> books, string readLine, out List<Book> returnBooks)
        {
            returnBooks = new List<Book>();
            var booksIds = ParseReadLine(readLine);
            foreach (var id in booksIds)
            {
                if (books.TryGetValue(id, out var foundBook))
                {
                    returnBooks.Add(foundBook);
                    Console.WriteLine(foundBook);
                }
            }
        }
        private static void SendNotifications(BookActions bookActions)
        {
            var sms = new SmsService();
            var email = new MailServvice();
            bookActions.BookNotification += sms.Notify;
            bookActions.BookNotification += email.Notify;
        }
        public static string[] ParseReadLine(string readLine)
        {
            return readLine.Split(';');
        }
    }
}