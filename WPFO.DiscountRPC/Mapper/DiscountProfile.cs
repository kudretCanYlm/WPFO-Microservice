using AutoMapper;
using WPFO.DiscountRPC.Entities;
using WPFOGRPCDiscount;

namespace WPFO.DiscountRPC.Mapper
{
	public class DiscountProfile:Profile
	{
		public DiscountProfile()
		{
			CreateMap<Coupon, CouponModel>().ReverseMap();
		}
	}
}
