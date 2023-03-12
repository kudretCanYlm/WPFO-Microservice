using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WPFO.Basket.Api.GrpcServices;
using WPFO.Basket.Entities;
using WPFO.Basket.Repositories.Interfaces;

namespace WPFO.Basket.Api.Controllers
{
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository basketRepository;
		private readonly ILogger<BasketController> _logger;
		private readonly IMapper mapper;
		private readonly DiscountGrpcService _discountGrpcService;
		private readonly IPublishEndpoint _publishEndpoint;

		public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger, IMapper mapper, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint)
		{
			this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
			_publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
		}

		[HttpGet]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
		{
			var basket = await basketRepository.GetBasket(userName);

			return Ok(basket);
		}

		[HttpDelete("{userName}/{productId}", Name = "DeleteShoppingCart")]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]		
		public async Task<IActionResult> DeleteShoppingCart(string userName,string ProductId)
		{
			await basketRepository.DeleteBasket(userName);

			return Ok();
		}
		
		[Route("[action]")]
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Accepted)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
		{
			// get existing basket with total price            
			// Set TotalPrice on basketCheckout eventMessage
			// send checkout event to rabbitmq
			// remove the basket
		
			// get existing basket with total price
			var basket = await basketRepository.GetBasket(basketCheckout.UserName);
			if (basket == null)
			{
				return BadRequest();
			}
		
			// send checkout event to rabbitmq
			var eventMessage = mapper.Map<BasketCheckoutEvent>(basketCheckout);
			eventMessage.TotalPrice = basket.TotalPrice;
			await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);
		
			// remove the basket
			await basketRepository.DeleteBasket(basket.UserName);
		
			return Accepted();
		}

	}
}
