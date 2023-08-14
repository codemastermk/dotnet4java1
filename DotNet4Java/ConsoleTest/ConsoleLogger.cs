namespace ConsoleTest
{
    public class ConsoleLogger : ICustomLogger
    {
        public void LogToConsole(string message)
        {
            Console.WriteLine(message);
        }

        public void LogToConsole(string message, string type)
        {
            LogToConsole($"{DateTime.Now}: {type} => {message}");
        }
    }
}