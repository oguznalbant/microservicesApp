using ECom.Product.Api.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ECom.Product.Api.Data.Abstract
{
    public class ProductContextSeed
    {
        public static void Seed(IMongoCollection<ProductEntity> productCollection)
        {
            var existedCollection = productCollection.Find(x => true).Any();
            if (!existedCollection)
            {
                var productDummyList = new List<ProductEntity>
                {
                    new ProductEntity
                    {
                        Name = "Urun-1",
                        Description ="Urun-Desc-1"
                    },
                    new ProductEntity
                    {
                        Name = "Urun-2",
                        Description ="Urun-Desc-2"
                    },
                    new ProductEntity
                    {
                        Name = "Urun-3",
                        Description ="Urun-Desc-3"
                    },
                    new ProductEntity
                    {
                        Name = "Urun-4",
                        Description ="Urun-Desc-4"
                    },
                    new ProductEntity
                    {
                        Name = "Urun-5",
                        Description ="Urun-Desc-5"
                    }
                };

                productCollection.InsertManyAsync(productDummyList); // todo: check
            }

        }
    }
}