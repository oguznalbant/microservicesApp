using ECom.Product.Api.Entities;
using ECom.Product.Api.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ECom.Product.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetProducts()
        {
            var products = await _productRepository.GetAll(x => true);

            return Ok(products);
        }
        
        [HttpGet("{id:length(24)}",Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductEntity>> GetProductById(string id) // todo: convention check 
        {
            var product = await _productRepository.Get(x => x.Id == id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}")] // after main controller route adding this routing
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetAll(x => x.CategoryName == category);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ProductEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProduct([FromBody] ProductEntity product)
        {
            await _productRepository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductEntity product) // todo: IActtionResult difference
        {
            await _productRepository.Update(product);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(ProductEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var result = await _productRepository.Delete(id);

            return Ok(result);
        }
    }
}
