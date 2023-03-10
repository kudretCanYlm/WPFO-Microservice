using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
        {
			//can change ElasticConfiguration
			string elasticUri = context.Configuration.GetSection("ElasticConfiguration:Uri").ToString();

            configuration
                 .Enrich.FromLogContext()
                 //.Enrich.WithMachineName()
                 .WriteTo.Debug()
                 .WriteTo.Console()
                 .WriteTo.Elasticsearch(
                     new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                     {
                         IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                         AutoRegisterTemplate = true,
                         NumberOfShards = 2,
                         NumberOfReplicas = 1
                     })
                 .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                 .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                 .CreateLogger();
        };
    }
}
