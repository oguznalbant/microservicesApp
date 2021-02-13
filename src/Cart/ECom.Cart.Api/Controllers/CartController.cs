using ECom.Cart.Api.Entities;
using ECom.Cart.Api.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ECom.Cart.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public CartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetCart(string username)
        {
            var cart = await _shoppingCartRepository.GetShoppingCart(username);

            return Ok(cart);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] ShoppingCart shoppingCart)
        {
            var cart = await _shoppingCartRepository.UpdateShoppingCart(shoppingCart); //todo patlicak mı ?

            return Ok(cart);
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string username)
        {
            var deleted = await _shoppingCartRepository.DeleteShoppingCart(username);

            return Ok(deleted);
        }
    }
}
