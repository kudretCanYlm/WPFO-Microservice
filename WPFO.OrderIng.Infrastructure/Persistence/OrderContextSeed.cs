using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFO.Ordering.Domain.Entities;

namespace WPFO.OrderIng.Infrastructure.Persistence
{
	public class OrderContextSeed
	{
		public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
		{
			int retryForAvailability = retry.Value;

			try
			{
				//orderContext.Database.

				if (!orderContext.Orders.Any())
				{
					orderContext.Orders.AddRange(GetPreconfiguredOrders());
					await orderContext.SaveChangesAsync();
				}
			}
			catch (Exception exception)
			{
				if (retryForAvailability < 50)
				{
					retryForAvailability++;
					var log = loggerFactory.CreateLogger<OrderContextSeed>();
					log.LogError(exception.Message);
					System.Threading.Thread.Sleep(2000);
					await SeedAsync(orderContext, loggerFactory, retryForAvailability);
				}
			}
		}

		private static IEnumerable<Order> GetPreconfiguredOrders()
		{
			return new List<Order>
			{
				new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
			};
		}
	}
}
