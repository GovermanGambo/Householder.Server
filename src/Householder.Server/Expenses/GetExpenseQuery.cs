using CQRS.Query.Abstractions;
using Householder.Server.Models;
using System.Collections.Generic;

namespace Householder.Server.Expenses
{
    public class GetExpenseQuery : IQuery<Expense>
    {
        public long Id { get; set; }
    }
}