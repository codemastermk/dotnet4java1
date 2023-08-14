namespace ConsoleTest
{
    public interface ICustomLogger
    {
        void LogToConsole(string message);
        void LogToConsole(string message, string type);
    }
}