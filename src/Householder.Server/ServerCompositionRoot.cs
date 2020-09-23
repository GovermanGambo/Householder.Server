using CQRS.Command.Abstractions;
using CQRS.LightInject;
using Householder.Server.Authentication;
using Householder.Server.Authorization;
using Householder.Server.Users;
using LightInject;

namespace Householder.Server
{
    public class ServerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterCommandHandlers()
                .RegisterQueryHandlers()
                .RegisterSingleton<ITokenManager, JwtTokenManager>()
                .RegisterSingleton<IPasswordManager, PasswordManager>()
                .RegisterSingleton<IPasswordPolicy, PasswordPolicy>()
                .Decorate<ICommandHandler<RegisterUserCommand>, RegisterUserValidator>();
        }
    }
}