namespace WebTest
{
    public class StartsWithConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
           if(values.TryGetValue(routeKey, out var routeValue))
            {
                if(routeValue is string stringValue)
                {
                    return stringValue.StartsWith("hello");
                }
            }

           return false;
        }
    }
}
