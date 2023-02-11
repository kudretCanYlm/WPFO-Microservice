using AutoMapper;
using WPFO.Discount2.Grpc.Entities;

namespace WPFO.Discount2.Grpc.Mapper
{
	public class DiscountProfile:Profile
	{
		public DiscountProfile()
		{
			CreateMap<Coupon, CouponModel>().ReverseMap();
		}
		
	}
}
