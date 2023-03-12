using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFO.Core.Repositories;

namespace WPFO.Ordering.Application.Features.Orders.Queries.GetOrderList
{
	public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, IEnumerable<OrdersVm>>
	{
		private readonly IOrderRepository orderRepository;
		private readonly IMapper mapper;

		public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<IEnumerable<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
		{
			var orderlist = await orderRepository.GetOrdersByUserName(request.UserName);
			return mapper.Map<IEnumerable<OrdersVm>>(orderlist);
		}
	}
}
