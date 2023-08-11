namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("New branch test 4");

            Dog dog = new Dog()
            {
                Id = 101,
                Name = "First Dog",
            };

            Dog dog2 = new Dog()
            {
                Id = 101,
                Name = "First Dog",
            };

            dog2 = dog;

            Point point1 = new Point()
            {
                X = 1,
                Y = 2
            };
            Point point2 = new Point();

            Point point3 = point1 with { Y = 3 };

            Console.WriteLine($"{point3.X}, {point3.Y}"); 
            Console.WriteLine($"{point1.X}, {point1.Y}");



            var record = new ClassAsRecord(1, "Test", "Test2");
            var record2 = new ClassAsRecord(1, "Test", "Test2");

            var record3 = record2 with { Id = 3 };

            //record2 = record;


            Console.WriteLine(dog);
            Console.WriteLine(point1);
            Console.WriteLine(record2);


            if (ReferenceEquals(record, record2))
            {
                Console.WriteLine($"Record references are the same!");
            }


            if (record3.Equals(record2))
            {
                Console.WriteLine($"Records are the same!");
            }


            if (point1.Equals(point2))
            {
                Console.WriteLine($"Points are the same!");
            }

            if (dog == dog2)
            {
                Console.WriteLine($"They are the same!");
            }

            if(dog is  { Id: > 100, Name: "test"})
            {
                Console.WriteLine($"this is dog with idasda: {dog.Id}");
                Console.WriteLine(dog.Description);
            }

            dog.Name = "test2";
            //dog.Id = 2;


            if(dog.Id == 1)
            {
                Console.WriteLine($"this is dog with id: {dog.Id}");
            }

            Console.ReadLine();
        }
    }
}