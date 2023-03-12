using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WPFO.Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using WPFO.Ordering.Application.Features.Orders.Queries.GetOrderList;

namespace WPFO.Ordering.API.Controllers
{
	[Route("api/v1/[controller]"),ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMediator mediator;

		public OrderController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet,ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
		{
			var query = new GetOrdersListQuery(userName);

			var orders = await mediator.Send(query);

			return Ok(orders);
		}

		[HttpPost, ProducesResponseType(typeof(OrdersVm),(int)HttpStatusCode.OK)]
		public async Task<ActionResult<OrdersVm>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
		{
			var result = await mediator.Send(command);
			return Ok(result);
		}

	}
}
