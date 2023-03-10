using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFO.Ordering.Application.Features.Orders.Queries.GetOrderList
{
	public class GetOrdersListQuery:IRequest<IEnumerable<OrdersVm>>
	{
		public string UserName { get; set; }

		public GetOrdersListQuery(string userName)
		{
			UserName = userName ?? throw new ArgumentNullException(nameof(userName));
		}
	}
}
