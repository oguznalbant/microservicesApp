using ECom.Order.Core.Entities;
using ECom.Order.Core.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Order.Core.Repository
{
    public interface IOrderRepository : IRepository<OrderEntity>
    {
        //Business method
        Task<IEnumerable<OrderEntity>> GetOrdersByUsername(string username);
    }
}
