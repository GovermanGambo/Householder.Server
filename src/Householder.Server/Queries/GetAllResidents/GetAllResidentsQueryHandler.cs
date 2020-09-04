using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;

namespace Householder.Server.Queries
{
    public class GetAllResidentsQueryHandler : IQueryHandler<GetAllResidentsQuery, IEnumerable<Resident>>
    {
        private IMySqlDatabase database;

        public GetAllResidentsQueryHandler(IMySqlDatabase database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<Resident>> Handle(GetAllResidentsQuery query)
        {
            List<Resident> results = new List<Resident>();
            var cmd = database.Connection.CreateCommand();

            cmd.CommandText = @"SELECT id, name FROM `resident`";

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new Resident(
                    reader.GetInt32("id"),
                    reader.GetString("name")
                ));
            }

            return results;
        }
    }
}