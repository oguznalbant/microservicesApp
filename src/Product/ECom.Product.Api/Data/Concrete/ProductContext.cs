using MongoDB.Driver;
using ECom.Product.Api.Settings.Abstract;
using ECom.Product.Api.Entities;

namespace ECom.Product.Api.Data.Abstract
{
    public class ProductContext : IProductContext
    {
        // Mongo Db Initialize and Collection served
        public ProductContext(IProductDatabaseSettings settings)
        {
            var conn = new MongoClient(settings.ConnectionString);
            var db = conn.GetDatabase(settings.DatabaseName);
            var productCollection = db.GetCollection<ProductEntity>(settings.CollectionName);

            Products = productCollection;
            ProductContextSeed.Seed(Products);
        }

        // Mongo Collections
        public IMongoCollection<ProductEntity> Products { get; }
    }
}
