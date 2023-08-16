using System.Text;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("New branch test 4");

            //Console.ReadLine();

            //Macchiato espresso = new Macchiato();

            //var coffees = espresso.GetCoffees().Take(5).ToList();


            //foreach (var e in coffees)
            //{
            //    Console.WriteLine($"Serving {e.Name}...");
            //}

            //foreach (var e in coffees)
            //{
            //    Console.WriteLine($"Serving {e.Name}...");
            //}

            ////Console.WriteLine(espresso.MakeCoffee());


            var book = new Book
            {
                Name = "Test book",
                Price = 102.656M,
            };
            book.Author = new Author
            {
                Name = "Test"
            };

            var sms = new SmsService();
            var mail = new MailServvice();

            book.BookNotification += sms.Notify;
            book.BookNotification += mail.Notify;
            book.BookNotification -= mail.Notify;
            book.BookNotification += Notify;


            List<int> myList = new List<int>();

            book.SellBook((x,y) =>
            {
                if( x> 100)
                {
                    return 0.95M;
                }
                return 1M;
            }); 

            decimal GetDiscount(decimal price)
            {
                if (price > 100)
                {
                    return 0.7M;
                }
                else if (price > 80)
                {
                    return 0.9M;
                }

                return 1;
            }
        }
        public static void Notify(object? sender, BookEventArgs eventArgs)
        {
            Console.WriteLine($"Notifying for the {eventArgs.Author.Name}");
            Console.WriteLine($"With message: {eventArgs.Message}");

        }
    }
}


public class BookEventArgs : EventArgs
{
    public Author Author { get; set; }
    public string Message { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }

    public Author Author { get; set; }


    public EventHandler<BookEventArgs> BookNotification;



    public void SellBook(Func<decimal, int, decimal> discountFunction)
    {
        Console.WriteLine($"Selling book : {Name} for price {Price}");

        var discount = discountFunction(Price, 4);

        Console.WriteLine($"The discount is {discount}");

        Price *= discount;

        BookNotification?.Invoke(this, new BookEventArgs { Author = Author, Message = $"We sold this book now" });
    }
}

public class MailServvice
{
    public void Notify(object? sender, BookEventArgs eventArgs)
    {
        Console.WriteLine($"Sending email for the {eventArgs.Author.Name}");
        Console.WriteLine($"With message: {eventArgs.Message}");

    }
}

public class SmsService
{
    public void Notify(object? sender, BookEventArgs eventArgs)
    {
        Console.WriteLine($"Sending an SMS for the {eventArgs.Author.Name}");
        Console.WriteLine($"With message: {eventArgs.Message}");

    }
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
}


internal class Macchiato
{
    public string Name { get; set; }


    public IEnumerable<Espresso> GetCoffees()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new Espresso(Name = $"New esspresso {i}");
        }
    }


    //public override string MakeCoffee()
    //{
    //    return base.MakeCoffee() + $" and adding milk...{MyFunction(5)}";

    //    string MyFunction(int number)
    //    {
    //        return number.ToString();
    //    }
    //}
}

public interface ISomeInterface
{
    public int Price { get; set; }
}