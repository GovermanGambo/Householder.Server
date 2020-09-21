using System.Data;
using System.Threading.Tasks;
using DbReader;
using CQRS.Command.Abstractions;
using System.Threading;
using Householder.Server.Database;

namespace Householder.Server.Residents
{
    public class AddResidentCommandHandler : ICommandHandler<AddResidentCommand>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public AddResidentCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(AddResidentCommand command, CancellationToken cancellationToken = default)
        {
            await dbConnection.ExecuteAsync(sqlProvider.InsertResident, command);

            command.Id = await dbConnection.ExecuteScalarAsync<long>(sqlProvider.GetLastInsertId);
        }
    }

    public class AddResidentCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}