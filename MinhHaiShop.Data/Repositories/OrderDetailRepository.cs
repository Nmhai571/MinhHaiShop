using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;

namespace MinhHaiShop.Data.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail> { }
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
