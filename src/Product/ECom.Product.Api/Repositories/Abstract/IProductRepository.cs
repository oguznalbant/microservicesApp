using ECom.Product.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECom.Product.Api.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<ProductEntity> Get(Expression<Func<ProductEntity, bool>> filter); //todo: task
        Task<IEnumerable<ProductEntity>> GetAll(Expression<Func<ProductEntity,bool>> filter);
        Task Create(ProductEntity product);
        Task<bool> Delete(string id);
        Task<bool> Update(ProductEntity product);
    }
}
