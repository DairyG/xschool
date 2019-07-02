using Microsoft.Extensions.DependencyInjection;
using XSchool.Businesses.Extensions;
using XSchool.Repositories.Extensions;
using XShop.WorkFlow.Repositories.Extensions;

namespace XShop.GCenter.Businesses.Extensions
{
    public static  class WorkFlowBusinessesServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinesses(this IServiceCollection services)
        {
            services.AddRepositories();
            var assembly = typeof(WorkFlowBusinessesServiceCollectionExtensions).Assembly;
            return services.AddBusiness(assembly);
        }
    }
}
