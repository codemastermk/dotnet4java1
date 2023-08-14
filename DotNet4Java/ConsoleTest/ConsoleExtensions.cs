namespace ConsoleTest.Extensions
{
    public static class ConsoleExtensions
    {
        public static void LogError(this ICustomLogger consoleLogger, string message)
        {
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            consoleLogger.LogToConsole(message, "Error");
            Console.ForegroundColor = colour;
        }

        public static void LogWarning(this ICustomLogger consoleLogger, string message)
        {
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            consoleLogger.LogToConsole(message, "Warning");
            Console.ForegroundColor = colour;
        }

        public static void LogInformation(this ICustomLogger consoleLogger, string message)
        {
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            consoleLogger.LogToConsole(message, "Information");
            Console.ForegroundColor = colour;
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null)
            {
                return true;
            }
            if (value.Trim().Length == 0)
            {
                return true;
            }
            return false;

        }
    }
}