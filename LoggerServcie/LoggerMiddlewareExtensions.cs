using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LoggerServcie
{
    public static class LoggerMiddlewareExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void GetLoggerConfiguarationFile()
        {
            try
            {

                LogManager.LoadConfiguration(@"C:\Users\Mundayadan\Source\Repos\LoggerServcie\nlog.config");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
