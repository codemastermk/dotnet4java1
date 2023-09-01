using System.Reflection.Metadata.Ecma335;

namespace ConsoleTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

           await PrintNumber(4);
            
           

            Console.WriteLine($"step 2");

            Console.ReadLine();
        }

        public static void FireAndForget(Action action)
        {
            action();
        }

        //public static void FireAndForget(Func<Task> action)
        //{
        //    action();
        //}

        public static async Task<int> PrintNumber(int number)
        {
            await Task.Delay(500);
            Console.WriteLine(number);
            var text = await File.ReadAllTextAsync("somefile.txt");

            return 3;

        }

        public static async void GetNameAsync()
        {
            Console.WriteLine("Staring with some long work");
            var text = await File.ReadAllTextAsync("somefile.txt");

            Console.WriteLine("Staring with some long work");

        }
    }
}
