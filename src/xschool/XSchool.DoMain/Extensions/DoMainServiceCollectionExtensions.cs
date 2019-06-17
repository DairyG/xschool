using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace XSchool.DoMain.Extensions
{
    public static class DoMainServiceCollectionExtensions
    {
        public static IServiceCollection AddDoMain(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (type.BaseType.FullName == "XSchool.DoMain.DoMainBase")
                {
                    services.AddScoped(type);
                }
            }
            return services;
        }
    }
}
