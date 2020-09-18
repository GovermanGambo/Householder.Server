using CQRS.LightInject;
using LightInject;

namespace Householder.Server
{
    public class ServerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterCommandHandlers()
                .RegisterQueryHandlers();
        }
    }
}