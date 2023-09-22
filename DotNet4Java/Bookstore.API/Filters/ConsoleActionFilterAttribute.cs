using Microsoft.AspNetCore.Mvc.Filters;

namespace Bookstore.API.Filters
{
    public class ConsoleActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly string name;
        public int Order;

        public ConsoleActionFilterAttribute(string name)
        {
            this.name = name;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"OnActionExecuted from {name}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"OnActionExecuting from {name}");
        }
    }
}
