using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;
using MySqlConnector;

namespace Householder.Server.Queries
{
    public class GetResidentQueryHandler : IQueryHandler<GetResidentQuery, Resident>
    {
        private IMySqlDatabase database;

        public GetResidentQueryHandler(IMySqlDatabase database)
        {
            this.database = database;
        }
        
        public async Task<Resident> Handle(GetResidentQuery query)
        {
            var id = query.ID;

            var cmd = database.Connection.CreateCommand();

            cmd.CommandText = @"SELECT id, name FROM `resident` WHERE id=@id";

            cmd.Parameters.Add(new MySqlParameter("@id", id));

            var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Resident(
                    reader.GetInt32("id"),
                    reader.GetString("name")
                );
            }
            else
            {
                return null;
            }
        }
    }
}