using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using DbReader;
using Householder.Server.Database;
using Newtonsoft.Json;

namespace Householder.Server.Users
{
    public class InsertUserCommandHandler : ICommandHandler<InsertUserCommand>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public InsertUserCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(InsertUserCommand command, CancellationToken cancellationToken = default)
        {
            await dbConnection.ExecuteAsync(sqlProvider.InsertUser, command);

            command.Id = await dbConnection.ExecuteScalarAsync<long>(sqlProvider.GetLastInsertId);
        }
    }

    public class InsertUserCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string HashedPassword { get; set; }
    }
}