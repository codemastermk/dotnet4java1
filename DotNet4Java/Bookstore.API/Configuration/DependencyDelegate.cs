using Bookstore.Services;

namespace Bookstore.API.Configuration
{
    public static class DependencyDelegate
    {
        public delegate ISerialNumber SerialNumberResolver(string typeName);

        public static void RegisterDelegateDependency(this IServiceCollection services)
        {
            services.AddTransient<SimpleSerialNumber>();
            services.AddTransient<SerialNumber>();

            services.AddTransient<SerialNumberResolver>(serviceProvider => name =>
            {
                return name switch
                {
                    "Simple" => serviceProvider.GetService<SimpleSerialNumber>(),
                    "Original" => serviceProvider.GetService<SerialNumber>(),
                    _ => throw new NotSupportedException(),
                };
            });
        }
    }
}
