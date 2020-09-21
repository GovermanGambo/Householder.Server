using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Residents
{
    public class GetResidentQueryHandler : IQueryHandler<GetResidentQuery, ResidentDTO>
    {
        private readonly IDbConnection dbConnection;

        private readonly ISqlProvider sqlProvider;

        public GetResidentQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<ResidentDTO> HandleAsync(GetResidentQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<ResidentDTO>(sqlProvider.GetResident, query);
            
            return results.SingleOrDefault();
        }
    }

    public class GetResidentQuery : IQuery<ResidentDTO>
    {
        public long Id { get; set; }
    }
}