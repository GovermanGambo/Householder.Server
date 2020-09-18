using Householder.Server.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Householder.Server.Host
{
    internal static class ServiceExtensions
    {
        public static ApplicationConfiguration AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationConfiguration = configuration.Get<ApplicationConfiguration>(conf => conf.BindNonPublicProperties = true);
            
            var connectionString = configuration.GetValue<string>("ConnectionString");

            services.AddSingleton(applicationConfiguration);

            return applicationConfiguration;
        }
    }
}