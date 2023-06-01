using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinhHaiShop.Data
{
    public static class DependencyInjection

    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMinhHaiShopDatabase(configuration)
                /*.RegisterServices()*/;
            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            /*services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();*/
            return services;
        }

        private static IServiceCollection RegisterMinhHaiShopDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MinhHaiShopDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("MinhHaiShopConnection")));
            return services;
        }
    }
}
