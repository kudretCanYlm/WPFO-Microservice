using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Common.Logging;
using Serilog;

namespace WPFO.Basket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSerilog(SeriLogger.Configure)
				.ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
