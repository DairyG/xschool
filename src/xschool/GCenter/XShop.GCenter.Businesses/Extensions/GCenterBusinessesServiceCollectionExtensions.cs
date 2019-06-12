using Microsoft.Extensions.DependencyInjection;
using XSchool.Businesses.Extensions;
using XSchool.Repositories.Extensions;
using XShop.GCenter.Repositories.Extensions;

namespace XShop.GCenter.Businesses.Extensions
{
    public static  class GCenterBusinessesServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinesses(this IServiceCollection services)
        {
            services.AddRepositories();
            var assembly = typeof(GCenterBusinessesServiceCollectionExtensions).Assembly;
            return services.AddBusiness(assembly);
        }
    }
}
