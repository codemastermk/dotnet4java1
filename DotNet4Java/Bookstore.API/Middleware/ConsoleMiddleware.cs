namespace Bookstore.API.Middleware
{
    public class ConsoleMiddleware : IMiddleware
    {
        private readonly ILogger<ConsoleMiddleware> _logger;

        public ConsoleMiddleware(ILogger<ConsoleMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Console middleware start");
            _logger.LogCritical("Console middleware start");

            await next(context);

            Console.WriteLine("Console middleware end");
        }
    }
}
