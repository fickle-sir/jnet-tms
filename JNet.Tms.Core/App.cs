using System;
using Microsoft.Extensions.Configuration;

namespace JNet
{
    public static class App
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        //public static ILoggerFactory LoggerFactory { get; private set; }

        public static IConfiguration Configuration { get; set; }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
