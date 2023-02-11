using AutoMapper;
using EventBus.Messages.Events;
using WPFO.Basket.Entities;

namespace WPFO.Basket.Mapper
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
