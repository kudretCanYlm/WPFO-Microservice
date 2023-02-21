using AutoMapper;
using EventBus.Messages;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using WPFO.Ordering.Application.Contracts.Persistence;
using WPFO.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace WPFO.Ordering.API.RabbitMQ
{
	public class EventBusRabbitMQConsumer
	{
		private readonly IRabbitMQConnection _connection;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IOrderRepository _repository;

		public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, IOrderRepository repository)
		{
			_connection = connection ?? throw new ArgumentNullException(nameof(connection));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public void Consume()
		{
			var channel = _connection.CreateModel();
			channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

			var consumer = new EventingBasicConsumer(channel);

			consumer.Received += ReceivedEvent;

			channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
			
		}

		private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
		{
			if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
			{
				var message= Encoding.UTF8.GetString(e.Body.Span);
				var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

				var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);
				var result = await _mediator.Send(command);
			}
		}

		public void Disconnect()
		{
			_connection.Dispose();
		}
	}
}
