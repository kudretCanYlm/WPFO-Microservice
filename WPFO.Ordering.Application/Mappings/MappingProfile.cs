using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFO.Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using WPFO.Ordering.Application.Features.Orders.Commands.UpdateOrder;
using WPFO.Ordering.Application.Features.Orders.Queries.GetOrderList;
using WPFO.Ordering.Domain.Entities;

namespace WPFO.Ordering.Application.Mappings
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrdersVm>().ReverseMap();
			CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
			CreateMap<Order, UpdateOrderCommand>().ReverseMap();
		}
	}
}
