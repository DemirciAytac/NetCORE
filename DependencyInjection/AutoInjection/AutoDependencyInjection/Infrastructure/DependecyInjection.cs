using AutoDependencyInjection.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoDependencyInjection.Infrastructure
{
    public static class DependenyInjection
    {
        /// <summary>
        /// Add Services to DO container automatically.
        /// </summary>
        /// <param name="services">Services</param>
        /// <returns>Service collection with registered services with their respective lifetime in the service container</returns>
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Transient Services

            var transientServiceType = typeof(ITransientService);

            // Get services inheriting transient service
            var transientServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(transientServiceType.IsAssignableFrom)
                .Where(p => p.IsClass && !p.IsAbstract)
                .Select(p => new
                {
                    Service = p.GetInterfaces().FirstOrDefault(),
                    Implementation = p
                });

            // Register each transient service for startup
            if (transientServices.Count() > 0)
            {
                foreach (var transientService in transientServices)
                {
                    if (transientServiceType.IsAssignableFrom(transientService.Service))
                    {
                        services.AddTransient(transientService.Service, transientService.Implementation);
                    }
                }
            }

            #endregion

            #region Scoped Services

            var scopedServiceType = typeof(IScopedService);

            // Get services inheriting scoped service
            var scopedServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(scopedServiceType.IsAssignableFrom)
                .Where(p => p.IsClass && !p.IsAbstract)
                .Select(p => new
                {
                    Service = p.GetInterfaces().FirstOrDefault(),
                    Implementation = p
                });

            foreach (var scopedService in scopedServices)
            {
                if (scopedServiceType.IsAssignableFrom(scopedService.Service))
                {
                    services.AddScoped(scopedService.Service, scopedService.Implementation);
                }
            }

            #endregion Scoped Services

            #region Singleton Services

            var singletonServiceType = typeof(ISingletonService);

            // Get services inheriting singleton service
            var singletonServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(singletonServiceType.IsAssignableFrom)
                .Where(p => p.IsClass && !p.IsAbstract)
                .Select(p => new
                {
                    Service = p.GetInterfaces().FirstOrDefault(),
                    Implementation = p
                });

            foreach (var singletonService in singletonServices)
            {
                if (singletonServiceType.IsAssignableFrom(singletonService.Service))
                {
                    services.AddSingleton(singletonService.Service, singletonService.Implementation);
                }
            }

            #endregion Singleton Services
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            return services;
        }


        }
}
