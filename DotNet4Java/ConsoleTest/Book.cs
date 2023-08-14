namespace ConsoleTest
{
    internal partial class Program
    {
        public class Book
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public bool IsAvailable(string name, out int id, out string description, out string user)
            {
                id = Id;
                description = Description;
                user = "Someone who got this book before you!";

                return true;
            }
            public bool IsAvailableAgain(string name, out int id, out string description, out string user)
            {
                id = Id + 3;
                description = Description + "IsAvailableAgain";
                user = "Someone who got this book before you! IsAvailableAgain";

                return true;
            }

            public override string ToString()
            {
                return $"Id is: {Id}, Name is: {Name}, Description is: {Description}";
            }
        }
    }
}