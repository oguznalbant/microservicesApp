using System;

namespace ECom.Product.Api.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public int CreatedBy { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
        
        public int ModifiedBy { get; set; }
    }
}
