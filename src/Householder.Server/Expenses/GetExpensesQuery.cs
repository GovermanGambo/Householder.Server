using CQRS.Query.Abstractions;
using Householder.Server.Models;
using System.Collections.Generic;

namespace Householder.Server.Expenses
{
    public class GetExpensesQuery : IQuery<IEnumerable<Expense>>
    {
        public int? Limit { get; set; }
        public int? Status { get; set; }
        public int? ResidentId { get; set; }
    }
}