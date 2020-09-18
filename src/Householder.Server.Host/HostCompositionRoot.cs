using Householder.Server;
using Householder.Server.Database;
using LightInject;

namespace Householder.Server.Host
{
    public class HostCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .RegisterFrom<DatabaseCompositionRoot>()
                .RegisterFrom<ServerCompositionRoot>();
        }
    }
}