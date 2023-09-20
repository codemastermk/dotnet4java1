
namespace Bookstore.Services
{
    public interface ISimpleSerialNumber : ISerialNumber
    {

    }

    public interface IOriginalSerialNumber : ISerialNumber
    {

    }

    public interface ISerialNumber
    {
        string GetSerialNumber();
    }

    public interface IComplexSerialNumber
    {
        List<string> GetComplexSerialNumber();
    }

    public class SerialNumber : ISerialNumber
    {
        private Guid Serial { get; }

        public SerialNumber()
        {
            Serial = Guid.NewGuid();
        }


        public string GetSerialNumber()
        {
            return $"Serial from Original serial number: {Serial}";
        }
    }

    public class SimpleSerialNumber : ISerialNumber
    {
        private Guid Serial { get; }

        public SimpleSerialNumber()
        {
            Serial = Guid.NewGuid();
        }

        public string GetSerialNumber()
        {
            return $"Serial from Simple: {Serial}";
        }
    }


    public class ComplexSerialNumber : IComplexSerialNumber
    {
        private readonly ISerialNumber _serialNumber;

        private Guid Serial { get; }

        public ComplexSerialNumber(ISerialNumber serialNumber)
        {
            Serial = Guid.NewGuid();
            _serialNumber = serialNumber;
        }


        public List<string> GetComplexSerialNumber()
        {
            return new List<string> { $"Serial from Complex {Serial}", _serialNumber.GetSerialNumber() };

        }
    }

    
}
