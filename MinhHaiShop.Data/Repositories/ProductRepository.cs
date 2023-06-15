using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;

namespace MinhHaiShop.Data.Repositories
{
    public interface IProductRepository
    {

    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory) { }

    }
}
