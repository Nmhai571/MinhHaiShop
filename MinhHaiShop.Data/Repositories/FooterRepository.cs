using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;

namespace MinhHaiShop.Data.Repositories
{
    public interface IFooterRepository : IRepository<Footer> { }
    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
