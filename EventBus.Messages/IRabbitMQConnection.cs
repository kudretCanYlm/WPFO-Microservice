using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
	public interface IRabbitMQConnection
	{
		bool IsConnected { get; }
		bool TryConnect();
		IModel CreateModel();
		void Dispose();
	}
}
