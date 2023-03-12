using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using WPFO.DiscountRPC.Extensions;

namespace WPFO.DiscountRPC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host=CreateHostBuilder(args).Build();
			host.MigrateDatabase<Program>();
			host.Run();
		}
		//will addd serilog
		// Additional configuration is required to successfully run gRPC on macOS.
		// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				//.UseSerilog(SeriLogger.Configure)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
