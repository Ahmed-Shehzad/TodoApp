using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.API.Configurations
{
    public static class MediatRServiceConfiguration
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            ServiceRegistrar.AddRequiredServices(services, new Microsoft.Extensions.DependencyInjection.MediatRServiceConfiguration());
            return services;
        }
    }
}