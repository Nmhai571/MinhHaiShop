using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;

namespace MinhHaiShop.Data.Repositories
{
    public interface IAccountRepository : IRepository<ApplicationUser>
    {
       
    }

    public class AccountRepository : RepositoryBase<ApplicationUser>, IAccountRepository
    {
       

        public AccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        
        }

    }
}
