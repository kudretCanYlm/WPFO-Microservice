using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WPFO.Core.Repositories.Base;
using WPFO.Core.Repositories;
using WPFO.OrderIng.Infrastructure.Persistence;
using WPFO.OrderIng.Infrastructure.Repositories.Base;
using WPFO.OrderIng.Infrastructure.Repositories;
using EventBus.Messages;
using RabbitMQ.Client;
using WPFO.Ordering.API.RabbitMQ;
using WPFO.Ordering.API.Extentions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WPFO.Ordering.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<OrderContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("OrderConnection")), ServiceLifetime.Singleton);
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
			services.AddTransient<IOrderRepository, OrderRepository>();
			services.AddAutoMapper(typeof(Startup));
			services.AddMediatR(typeof(Startup));

			services.AddSingleton<IRabbitMQConnection>(sp =>
			{
				var factory = new ConnectionFactory()
				{
					HostName = Configuration["EventBus:HostName"]
				};

				if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
				{
					factory.UserName = Configuration["EventBus:UserName"];
				}

				if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
				{
					factory.Password = Configuration["EventBus:Password"];
				}

				return new RabbitMQConnection(factory);
			});

			services.AddSingleton<EventBusRabbitMQConsumer>();

			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WPFO.Ordering.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WPFO.Ordering.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

			app.UseRabbitListener();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
