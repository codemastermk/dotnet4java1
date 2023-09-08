namespace ConsoleTest
{
    using Book;

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
     
             
            var bookRepository = new BookRepository();

            foreach (var book in await bookRepository.GetBooks())
            {
                try
                {
                    var length = book.ISBN.Length;
                    Console.WriteLine(book.Name);
                    Console.WriteLine(book.ISBN);
                    Console.WriteLine(book.Id);

                    var i = length;
                }
                catch (Exception ex)
                {

                }

            }
            Console.ReadLine();
        }
    }   
}