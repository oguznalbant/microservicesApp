using AutoMapper;
using ECom.Cart.Api.Entities;
using ECom.Cart.Api.Repository.Abstract;
using ECom.EventBusRabbitMq.Common;
using ECom.EventBusRabbitMq.Events;
using ECom.EventBusRabbitMq.Producer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace ECom.Cart.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        //private readonly ILogger _logger; // will be implement
        private readonly IEventBusProducer<ShoppingCartCheckoutEvent> _eventBusProducer;

        public CartController(
            IShoppingCartRepository shoppingCartRepository, 
            IMapper mapper, 
            IEventBusProducer<ShoppingCartCheckoutEvent> eventBusProducer)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _eventBusProducer = eventBusProducer;
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

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> ShoppingCartCheckout([FromBody] ShoppingCart shoppingCart)
        {
            //get cart from redis
            var cart = await _shoppingCartRepository.GetShoppingCart(shoppingCart.Username);
            if (cart == null)
            {
                return BadRequest();
            }

            // delete cart from db(redis)
            var removedCart = await _shoppingCartRepository.DeleteShoppingCart(cart.Username);
            if (!removedCart)
            {
                return BadRequest();
            }

            var publishEvent = _mapper.Map<ShoppingCartCheckoutEvent>(shoppingCart);

            try
            {
                _eventBusProducer.Publish(EventBusConstants.BasketCheckoutQueue, publishEvent);
            }
            catch (System.Exception ex)
            {
                //_logger.LogError($"Event bus publishing {EventBusConstants.BasketCheckoutQueue} is not successful. Error: {ex}");
            }

            return Accepted();
        }
    }
}
