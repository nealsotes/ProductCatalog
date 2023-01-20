using Microsoft.Extensions.Logging;

namespace ProductCatalogAPI
{
    public static class LoggingConfiguration
    {
        public static void AddLogging(ILoggingBuilder builder)
        {
            builder.AddConsole();
            builder.ClearProviders();
            
        }
    }
}
