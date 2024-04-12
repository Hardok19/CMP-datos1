using Microsoft.Extensions.Logging;

namespace loggings{
    public static class Logger{
        // Instancia de ILoggerFactory utilizando el método Create de LoggerFactory.
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }

}

