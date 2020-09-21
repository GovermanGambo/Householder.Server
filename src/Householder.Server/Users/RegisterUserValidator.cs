using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Householder.Server.Authentication;

namespace Householder.Server.Users
{
    public class RegisterUserValidator : ICommandHandler<RegisterUserCommand>
    {
        private readonly ICommandHandler<RegisterUserCommand> commandHandler;
        private readonly IPasswordPolicy passwordPolicy;

        public RegisterUserValidator(ICommandHandler<RegisterUserCommand> commandHandler, IPasswordPolicy passwordPolicy)
        {
            this.commandHandler = commandHandler;
            this.passwordPolicy = passwordPolicy;
        }        
        public async Task HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            passwordPolicy.ApplyPolicy(command.Password, command.ConfirmPassword);
            await commandHandler.HandleAsync(command);
        }
    }

}