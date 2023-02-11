using System.Threading.Tasks;
using WPFO.Discount2.Grpc.Entities;

namespace WPFO.Discount2.Grpc.Repositories.interfaces
{
	public interface IDiscountRepository
	{
		Task<Coupon> GetDiscount(string productName);

		Task<bool> CreateDiscount(Coupon coupon);
		Task<bool> UpdateDiscount(Coupon coupon);
		Task<bool> DeleteDiscount(string productName);
	}
}
