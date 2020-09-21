using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Residents
{
    public class GetResidentByIdQueryHandler : IQueryHandler<GetResidentByIdQuery, ResidentDTO>
    {
        private readonly IDbConnection dbConnection;

        private readonly ISqlProvider sqlProvider;

        public GetResidentByIdQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<ResidentDTO> HandleAsync(GetResidentByIdQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<ResidentDTO>(sqlProvider.GetResidentById, query);
            
            return results.SingleOrDefault();
        }
    }

    public class GetResidentByIdQuery : IQuery<ResidentDTO>
    {
        public long Id { get; set; }
    }
}