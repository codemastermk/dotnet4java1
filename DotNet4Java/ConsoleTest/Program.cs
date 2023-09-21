using ConsoleTest.model;
using ConsoleTest.service;
using ConsoleTest.service.impl;

namespace ConsoleTest
{
    internal class Program
    {
        enum Choice { LIST, VIEW, SELL, EXIT }
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                // Perform cleanup operations here
                Console.WriteLine("Cleaning up...");
                Environment.Exit(0);
            };
            BookStoreService BookStoreService = InitBookStore();

            Console.WriteLine("Welcome to the BookStore!");
            while (true)
            {
                DisplayMenu();
                byte Choice = Convert.ToByte(Console.ReadLine());
                ProcessChoice((Choice)Choice - 1, BookStoreService);
            }

        }

        private static BookStoreService InitBookStore()
        {
            BookStore BookStore = new BookStore();

            Author MikiMaus, MiniMaus, DonaldDuck;
            CreateAuthors(out MikiMaus, out MiniMaus, out DonaldDuck);

            Book CodeCraft, DotNet, Java;
            CreateBooks(BookStore, MikiMaus, MiniMaus, DonaldDuck, out CodeCraft, out DotNet, out Java);

            PopulateBookStore(BookStore, CodeCraft, DotNet, Java);

            return new BookStoreServiceImpl(BookStore);
        }

        private static void PopulateBookStore(BookStore BookStore, Book CodeCraft, Book DotNet, Book Java)
        {
            BookStore.Books.Add(CodeCraft);
            BookStore.Books.Add(DotNet);
            BookStore.Books.Add(Java);
        }

        private static void CreateBooks(BookStore BookStore, Author MikiMaus, Author MiniMaus, Author DonaldDuck, out Book CodeCraft, out Book DotNet, out Book Java)
        {
            CodeCraft = new Book(MikiMaus, BookStore, 2.3m, "CodeCraft", "Book for developing code");
            DotNet = new Book(MiniMaus, BookStore, 5.3m, "DotNet", "Book for developing code C#");
            Java = new Book(DonaldDuck, BookStore, 6.3m, "Java", "Book for developing code Java");
        }

        private static void CreateAuthors(out Author MikiMaus, out Author MiniMaus, out Author DonaldDuck)
        {
            MikiMaus = new Author("Miki Maus");
            MiniMaus = new Author("Mini Maus");
            DonaldDuck = new Author("Donald Duck");
        }

        private static void ProcessChoice(Choice choice, BookStoreService BookStoreService)
        {

            switch (choice)
            {
                case Choice.LIST: { ListBooks(BookStoreService); break; }

                case Choice.VIEW: { ViewBook(BookStoreService); break; }

                case Choice.SELL: { SellBook(BookStoreService); break; }

                case Choice.EXIT: { ExitApplication(); break; }
            }
        }

        private static void SellBook(BookStoreService bookStoreService)
        {
            Console.WriteLine("Type the title of the book to sell:");

            string title = Console.ReadLine();

            bookStoreService.SellBook(title);
        }

        private static void ViewBook(BookStoreService bookStoreService)
        {
            Console.WriteLine("Type the title of the book to view: ");
            string title = Console.ReadLine();
            Console.WriteLine(bookStoreService.GetBook(title));
        }

        private static void ExitApplication()
        {
            // Perform cleanup operations here
            Console.WriteLine("Cleaning up...");
            Environment.Exit(0);
        }

        private static void ListBooks(BookStoreService BookStoreService)
        {
            BookStoreService.PrintBooks();
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Please choose one option below:");

            Console.WriteLine("1. List Books ");

            Console.WriteLine("2. View Book ");

            Console.WriteLine("3. Sell Book ");

            Console.WriteLine("4. Exit from BookStore!");
        }
    }
}