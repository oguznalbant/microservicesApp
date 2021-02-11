using ECom.Product.Api.Data.Abstract;
using ECom.Product.Api.Entities;
using ECom.Product.Api.Repositories.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECom.Product.Api.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _productContext;

        public ProductRepository(IProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task Create(ProductEntity product) // todo: async-await-task
        {
            await _productContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<ProductEntity>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _productContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<ProductEntity> Get(Expression<Func<ProductEntity, bool>> filter)
        {
            return await _productContext.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetAll(Expression<Func<ProductEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _productContext.Products.Find(filter).ToListAsync();
            }

            return await _productContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> Update(ProductEntity product)
        {
            var updateResult = await _productContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
