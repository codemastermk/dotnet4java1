using System.Globalization;

namespace Bookstore.API.Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Middleware start");
            var cultureString = context.Request.Query["culture"];
            if(!string.IsNullOrEmpty(cultureString))
            {
                var culture = new CultureInfo(cultureString);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            await _next(context);
            Console.WriteLine("Middleware End");
        }

    }
}
