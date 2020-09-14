using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;
using Householder.Server.Models;

namespace Householder.Server.Residents
{
    public class GetResidentQueryHandler : IQueryHandler<GetResidentQuery, Resident>
    {
        private readonly IDbConnection dbConnection;

        private readonly ISqlProvider sqlProvider;

        public GetResidentQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<Resident> HandleAsync(GetResidentQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<Resident>(sqlProvider.GetResident, query);
            
            return results.SingleOrDefault();
        }
    }
}