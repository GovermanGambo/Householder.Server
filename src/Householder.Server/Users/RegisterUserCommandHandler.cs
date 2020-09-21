using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using DbReader;
using Householder.Server.Authentication;
using Householder.Server.Database;
using Newtonsoft.Json;

namespace Householder.Server.Users
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IPasswordManager passwordManager;

        public RegisterUserCommandHandler(ICommandExecutor commandExecutor, IPasswordManager passwordManager)
        {
            this.commandExecutor = commandExecutor;
            this.passwordManager = passwordManager;
        }

        public async Task HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            var hashedPassword = passwordManager.HashPassword(command.Password);
            var insertUserCommand = new InsertUserCommand
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                RegisterDate = command.RegisterDate,
                HashedPassword = hashedPassword
            };
            await commandExecutor.ExecuteAsync(insertUserCommand);
            command.Id = insertUserCommand.Id;
        }
    }

    public class RegisterUserCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}