using System.Threading;
using System.Threading.Tasks;

namespace WPFO.Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
	public interface ICheckoutOrderCommandHandler
	{
		Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken);
	}
}