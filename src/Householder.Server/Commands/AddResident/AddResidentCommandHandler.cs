using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;
using MySqlConnector;

namespace Householder.Server.Commands
{
    public class AddResidentCommandHandler : ICommandHandler<AddResidentCommand, long>
    {
        private IMySqlDatabase database;

        public AddResidentCommandHandler(IMySqlDatabase database)
        {
            this.database = database;
        }

        public async Task<long> Handle(AddResidentCommand command)
        {
            var resident = command.Resident;

            var cmd = database.Connection.CreateCommand();

            cmd.CommandText = @"INSERT INTO `resident` (`name`) VALUES (@name)";

            cmd.Parameters.Add(new MySqlParameter("@name", resident.Name));
            
            try
            {
                await cmd.ExecuteNonQueryAsync();
                return cmd.LastInsertedId;
            }
            catch(MySqlException ex)
            {
                if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return -1;
                }
                else if (ex.ErrorCode == MySqlErrorCode.DataTooLong)
                {
                    return -2;
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}