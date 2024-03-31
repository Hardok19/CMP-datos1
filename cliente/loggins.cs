using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace loggings{
    public static class Logger
    {
        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole(); // Puedes cambiar esto por otro proveedor de logging según tus necesidades
        });

        public static ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }
}

