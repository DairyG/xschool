using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XSchool.Core;

namespace XSchool.Businesses.Extensions
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (type.BaseType.IsGenericType && typeof(IModel<int>).IsAssignableFrom(type.BaseType.GetGenericArguments()[0]))
                {
                    //services.AddDynamicProxy(type);
                    services.AddScoped(type);
                }
                //append bussiness wrapper
                if (type.BaseType == typeof(BusinessWrapper))
                {
                    services.AddScoped(type);
                }
            }
            return services;
        }
    }
}
