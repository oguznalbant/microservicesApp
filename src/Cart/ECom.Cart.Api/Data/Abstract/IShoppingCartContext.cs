using StackExchange.Redis;

namespace ECom.Cart.Api.Data.Abstract
{
    public interface IShoppingCartContext
    {
        IDatabase Redis { get; }
    }
}
