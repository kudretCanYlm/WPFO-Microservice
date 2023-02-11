using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WPFO.Basket.Entities;
using WPFO.Basket.Repositories.Interfaces;

namespace WPFO.Basket.Api.Controllers
{
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository basketRepository;
		private readonly ILogger<BasketController> _logger;

		public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger)
		{
			this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

		[HttpPut]
		[ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
		public async Task<IActionResult> UpdateShoppingCartByUserName([FromBody] )
		{

		}

	}
}
