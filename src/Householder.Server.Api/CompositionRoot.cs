using Householder.Server.Api.Controllers;
using Householder.Server.DataAccess;
using Householder.Server.Queries;
using LightInject;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Householder.Server.Api
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            var configuration = GetConfiguration();
            serviceRegistry.Register<IConfigurationRoot>(c => configuration);
    
            serviceRegistry.Register<IMySqlDatabase>(c => new MySqlDatabase(configuration["ConnectionStrings:DefaultConnection"]));

            // Register query and command handlers
            serviceRegistry.RegisterCommandHandlers();
            serviceRegistry.RegisterQueryHandlers();
        
            // Register controllers
            serviceRegistry.Register<ResidentController>(c => new ResidentController(c.GetInstance<IQueryProcessor>(), null));
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .AddJsonFile("appsettings.json", true);

            return builder.Build();
        }
    }
}