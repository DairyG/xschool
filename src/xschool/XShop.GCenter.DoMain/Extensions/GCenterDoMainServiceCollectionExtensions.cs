using Microsoft.Extensions.DependencyInjection;
using XSchool.DoMain.Extensions;
using XShop.GCenter.Repositories.Extensions;

namespace XShop.GCenter.DoMain.Extensions
{
    public static class GCenterDoMainServiceCollectionExtensions
    {
        public static IServiceCollection AddDoMain(this IServiceCollection services)
        {
            services.AddRepositories();

            var assembly = typeof(GCenterDoMainServiceCollectionExtensions).Assembly;
            return services.AddDoMain(assembly);
        }
    }
}
