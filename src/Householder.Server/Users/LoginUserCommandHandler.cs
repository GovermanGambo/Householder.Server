using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using Householder.Server.Authentication;
using Householder.Server.Authorization;
using Newtonsoft.Json;

namespace Householder.Server.Users
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IPasswordManager passwordManager;
        private readonly ITokenManager tokenManager;

        public LoginUserCommandHandler(IQueryExecutor queryExecutor, IPasswordManager passwordManager, ITokenManager tokenManager)
        {
            this.queryExecutor = queryExecutor;
            this.passwordManager = passwordManager;
            this.tokenManager = tokenManager;
        }

        public async Task HandleAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
        {
            var getUserQuery = new GetUserByEmailQuery { Email = command.Email };
            var user = await queryExecutor.ExecuteAsync(getUserQuery);

            if (user == null || !passwordManager.VerifyPassword(command.Password, user.HashedPassword))
            {
                throw new AuthenticationFailedException("Invalid username or password.");
            }
            
            var token = tokenManager.GenerateToken(user);
            command.Token = token;
        }
    }

    public class LoginUserCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string Token { get; set; }
    }
}