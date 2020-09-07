using System.Collections.Generic;
using Householder.Server.Models;

namespace Householder.Server.Queries
{
    public class GetExpensesByResidentQuery : IQuery<IEnumerable<Expense>>
    {
        public int ResidentID { get; }

        public GetExpensesByResidentQuery(int residentId)
        {
            ResidentID = residentId;
        }
    }
}