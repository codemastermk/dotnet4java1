namespace Bookstore.API.Configuration
{
    public interface ISomeLogger
    {

    }
    public class SomeLoggerClass : ISomeLogger
    {
        private readonly ILogger<SomeLoggerClass> logger;

        public SomeLoggerClass(ILogger<SomeLoggerClass> logger) 
        {
            this.logger = logger;
        }
    }
   
    public class SomeOtherLoggerClass : ISomeLogger
    {
        private readonly ILogger<SomeOtherLoggerClass> logger;

        public SomeOtherLoggerClass(ILogger<SomeOtherLoggerClass> logger)
        {
            this.logger = logger;
        }
    }

    public class SomeLoggerFactory
    {
        private readonly ILoggerFactory loggerFactory;

        public SomeLoggerFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public ISomeLogger CreateLoggerClass(string name)
        {
            return new SomeLoggerClass(this.loggerFactory.CreateLogger<SomeLoggerClass>());

            return new SomeOtherLoggerClass(this.loggerFactory.CreateLogger<SomeOtherLoggerClass>());
        }
    }
}
