using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;

namespace MinhHaiShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order> { }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
