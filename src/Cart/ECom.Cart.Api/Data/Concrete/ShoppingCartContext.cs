using ECom.Cart.Api.Data.Abstract;
using StackExchange.Redis;

namespace ECom.Cart.Api.Data.Concrete
{
    public class ShoppingCartContext : IShoppingCartContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public ShoppingCartContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = _redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
