using Bookstore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.API.Configuration
{
    public interface IDependencyFactory
    {
        ISerialNumber GetDependency(string name);
    }
    public class DependencyFactory : IDependencyFactory
    {
        private readonly IEnumerable<ISerialNumber> _serialNumbers;

        public DependencyFactory(IEnumerable<ISerialNumber> serialNumbers)
        {
            _serialNumbers = serialNumbers;
        }

        public ISerialNumber GetDependency(string name)
        {
            return name switch
            {
                "Simple" => _serialNumbers.Where(x => x.GetType() == typeof(SimpleSerialNumber)).First(),
                "Original" => _serialNumbers.Where(x => x.GetType() == typeof(SerialNumber)).First(),
                _ => throw new NotSupportedException(),
            };
        }
    }
}
