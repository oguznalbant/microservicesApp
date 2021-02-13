using ECom.Cart.Api.Data.Abstract;
using ECom.Cart.Api.Entities;
using ECom.Cart.Api.Repository.Abstract;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ECom.Cart.Api.Repository.Concrete
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IShoppingCartContext _shoppingCartContext;

        public ShoppingCartRepository(IShoppingCartContext shoppingCartContext)
        {
            _shoppingCartContext = shoppingCartContext;
        }

        public async Task<ShoppingCart> GetShoppingCart(string userName)
        {
            var redisValue = await _shoppingCartContext.Redis.StringGetAsync(userName);

            if (!redisValue.HasValue)
            {
                return null;
            }

            var convertedRedisValue = JsonConvert.DeserializeObject<ShoppingCart>(redisValue);
            return convertedRedisValue;
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            var serializedValue = JsonConvert.SerializeObject(shoppingCart);

            var updatedObject = await _shoppingCartContext.Redis.StringSetAsync(shoppingCart.Username, serializedValue);

            if (!updatedObject)
            {
                return null;
            }

            var updatedCart = await GetShoppingCart(shoppingCart.Username);
            return updatedCart;
        }

        public async Task<bool> DeleteShoppingCart(string userName)
        {
            var deleted = await _shoppingCartContext.Redis.KeyDeleteAsync(userName);

            return deleted;
        }
    }
}
