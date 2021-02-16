using AutoMapper;
using ECom.Cart.Api.Entities;
using ECom.EventBusRabbitMq.Events;

namespace ECom.Cart.Api.Mappings
{
    public class ShoppingCartCheckoutMapping : Profile
    {
        public ShoppingCartCheckoutMapping()
        {
            CreateMap<ShoppingCart, ShoppingCartCheckoutEvent>().ReverseMap();
        }
    }
}
