using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XSchool.Core;

namespace XSchool.Repositories.Extensions
{
    public static class RepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (type.BaseType.IsGenericType && typeof(IModel<int>).IsAssignableFrom(type.BaseType.GetGenericArguments()[0]) && !type.IsAbstract)
                {
                    //services.AddDynamicProxy(type);
                    services.AddScoped(type);
                }
            }
            return services;
        }
    }
}
