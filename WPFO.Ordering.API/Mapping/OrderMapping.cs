using AutoMapper;
using EventBus.Messages.Events;
using WPFO.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace WPFO.Ordering.API.Mapping
{
	public class OrderMapping:Profile
	{
		public OrderMapping()
		{
			CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();

		}
	}
}
