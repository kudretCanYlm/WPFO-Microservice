using AutoMapper;
using WPFO.Discount.Grpc.Entities;

namespace WPFO.Discount.Grpc.Mapper
{
    public class DiscountProfile:Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
