using ECom.Order.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECom.Order.Infastructure.Data
{
    public class OrderDataContext : DbContext
    {
        public OrderDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
