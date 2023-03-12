using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFO.Core.Repositories;
using WPFO.OrderIng.Infrastructure.Repositories.Base;
using WPFO.Ordering.Domain.Entities;
using WPFO.OrderIng.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WPFO.OrderIng.Infrastructure.Repositories
{
	public class OrderRepository: Repository<Order>, IOrderRepository
	{
		public OrderRepository(OrderContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
		{
			var orderList = await _dbContext.Orders
								.Where(o => o.UserName == userName)
								.ToListAsync();
			return orderList;
		}
		
	}
}
