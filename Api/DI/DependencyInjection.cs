using Core.Interfaces;
using System.Reflection;

namespace Api.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblies = new[]
            {
            Assembly.GetAssembly(typeof(Application.Common.Mappings.MappingProfile)), // Application
            Assembly.GetAssembly(typeof(Infrastructure.Data.EnterpriseDbContext)) // Infrastructure
        }.Where(a => a != null).Cast<Assembly>().ToArray();

            foreach (var assembly in assemblies)
            {
                var serviceTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i == typeof(IService)))
                    .ToList();

                foreach (var serviceType in serviceTypes)
                {
                    var serviceInterface = serviceType.GetInterfaces()
                        .FirstOrDefault(i => i != typeof(IService) && i.Name.EndsWith("Service"));
                    if (serviceInterface != null)
                    {
                        services.AddScoped(serviceInterface, serviceType);
                    }
                }
            }

            return services;
        }
    }
}
