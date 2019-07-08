using Microsoft.Extensions.DependencyInjection;
using XSchool.Repositories.Extensions;


namespace XShop.WorkFlow.Repositories.Extensions
{
    public static class GCenterRepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var assembly = typeof(GCenterRepositoriesServiceCollectionExtensions).Assembly;
            return services.AddRepositories(assembly);
        }
    }
}
