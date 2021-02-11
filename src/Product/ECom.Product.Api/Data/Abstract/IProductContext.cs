using ECom.Product.Api.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ECom.Product.Api.Data.Abstract
{
    public interface IProductContext
    {
        IMongoCollection<ProductEntity> Products { get; }
    }
}
