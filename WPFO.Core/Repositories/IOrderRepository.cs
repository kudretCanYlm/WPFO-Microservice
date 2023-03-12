using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFO.Core.Repositories.Base;
using WPFO.Ordering.Domain.Entities;

namespace WPFO.Core.Repositories
{
	public interface IOrderRepository:IRepository<Order>
	{
		Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
	}
}
