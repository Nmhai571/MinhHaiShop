namespace MinhHaiShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private MinhHaiShopDbContext context;
        public MinhHaiShopDbContext Init()
        {
            return context ?? (context = new MinhHaiShopDbContext());
        }
        protected override void DisposeCore()
        {
            if(context != null)
            {
                context.Dispose();
            }
        }
    }
}
