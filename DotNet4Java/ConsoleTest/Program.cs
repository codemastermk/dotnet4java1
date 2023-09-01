using System.Net.WebSockets;

namespace ConsoleTest
{
    public class Book
    {
        public Book()
        {

        }
    }

    public class BookFactory
    {
        private readonly HttpClient client;

        public BookFactory(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Book> Create()
        {
            await client.GetStringAsync("");
            return new Book();
        }
    }


    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Step 1: {Thread.CurrentThread.ManagedThreadId}");
            var client = new HttpClient();

            Console.WriteLine($"Step 2: {Thread.CurrentThread.ManagedThreadId}");
            var httpTask = client.GetStringAsync("http://google.com");


            httpTask.GetAwaiter().GetResult();


            Console.WriteLine($"Step 3: {Thread.CurrentThread.ManagedThreadId}");

            long count = 1;
            for (long i = 0; i < 1_000_000; i++)
            {
                count += i;
            }

            Console.WriteLine($"Step 4: {Thread.CurrentThread.ManagedThreadId}");

            var text = await httpTask;

            Console.WriteLine($"Step 5: {Thread.CurrentThread.ManagedThreadId}");
            //Console.WriteLine($"Step 1: {Thread.CurrentThread.ManagedThreadId}");


            //Console.WriteLine("Hello, World!");

            var task = MakeTeaAsync();

            Console.WriteLine("Hello, World ww!");

            await task;

            var isad = await GetFullName();

            Console.ReadLine();
        }


        public static async Task<string> GetFullName()
        {
            Console.WriteLine("Getting the name!");
             GetLastName();
            return "";
        }

        public static async void GetLastName()
        {
            Console.WriteLine("Getting the name!");
        }

        private static async Task<string> MakeTeaAsync()
        {
            var boiledWaterTaks = BoilWaterAsync();
            Console.WriteLine("Take the cups out!");
            Console.WriteLine("Prepare the tea!");

            long count = 1;
            for (long i = 0; i < 1_000_000_000; i++)
            {
                count += i;
            }

            Console.WriteLine("Put the tea in the cups");

            var boiledWater = await boiledWaterTaks;

            var tea = $"Pour {boiledWater} in the cups";
            Console.WriteLine(tea);

            return tea;
        }

        private static async Task<string> BoilWaterAsync()
        {
            Console.WriteLine("The kettle is started!");
            Console.WriteLine("The water is heating up!");

            await Task.Delay(300);

            Console.WriteLine("The kettle finished boiling!");

            return "boiled water";
        }
    }
}