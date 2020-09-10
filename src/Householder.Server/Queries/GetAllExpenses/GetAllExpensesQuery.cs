using Householder.Server.Models;
using System.Collections.Generic;

namespace Householder.Server.Queries
{
    public class GetAllExpensesQuery : IQuery<IEnumerable<Expense>>
    {
        public int? Limit { get; }
        public int? Status { get; }
        public int? ResidentId { get; }
        public GetAllExpensesQuery(int? limit, int? status, int? residentId)
        {
            Limit = limit;
            Status = status;
            ResidentId = residentId;
        }
    }
}