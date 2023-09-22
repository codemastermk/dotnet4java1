using Microsoft.AspNetCore.Mvc.Filters;
using System.Xml.Linq;

namespace Bookstore.API.Filters
{
    public class ConsoleAsyncActionFilterAtrribute : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        private readonly string name;
        public int Order { get; set; }

        public ConsoleAsyncActionFilterAtrribute(string name)
        {
            this.name = name;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
            Console.WriteLine($"OnActionExecution Async from {name}");

            await next();

            Console.WriteLine($"OnActionExecution Async from {name}");
        }
    }
}
