using ECom.Product.Api.Settings.Abstract;

namespace ECom.Product.Api.Settings.Concrete
{
    // provides settings sections for product database
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
