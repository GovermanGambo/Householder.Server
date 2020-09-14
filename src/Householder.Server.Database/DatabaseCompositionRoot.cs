using DbReader;
using Householder.Server.Abstractions;
using LightInject;
using MySqlConnector;
using ResourceReader;
using System.Data;

namespace Householder.Server.Database
{
    public class DatabaseCompositionRoot : ICompositionRoot
    {
        static DatabaseCompositionRoot()
        {
            DbReaderOptions.WhenReading<long?>().Use((rd, i) => rd.GetInt32(i));
            DbReaderOptions.WhenReading<long>().Use((rd, i) => rd.GetInt32(i));
            DbReaderOptions.WhenReading<string>().Use((rd, i) => (string)rd.GetValue(i));
            DbReaderOptions.WhenReading<bool>().Use((rd, i) => rd.GetInt32(i) != 0);
        }
        
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .RegisterScoped(CreateConnection)
                .RegisterSingleton(_ => new ResourceBuilder().Build<ISqlProvider>());
        }

        private static IDbConnection CreateConnection(IServiceFactory serviceFactory)
        {
            var configuration = serviceFactory.GetInstance<ApplicationConfiguration>();
            var connection = new MySqlConnection(configuration.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}