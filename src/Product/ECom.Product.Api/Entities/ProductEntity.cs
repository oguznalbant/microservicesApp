using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ECom.Product.Api.Entities
{
    public class ProductEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public string CategoryName { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public List<ProductType> Types { get; set; } = new List<ProductType>();

        public DateTime CreatedAt { get; set; }
        
        public int CreatedBy { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
        
        public int ModifiedBy { get; set; }
    }
}
