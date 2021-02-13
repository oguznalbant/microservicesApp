using ECom.Cart.Api.Entities;
using System.Threading.Tasks;

namespace ECom.Cart.Api.Repository.Abstract
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCart(string userName);
     
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart);
        
        Task<bool> DeleteShoppingCart(string userName);
    }
}
