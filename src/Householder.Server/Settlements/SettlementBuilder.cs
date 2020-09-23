using System.Linq;
using System.Collections.Generic;
using System;
using Householder.Server.Expenses;
using Householder.Server.Residents;
using Householder.Server.Users;

namespace Householder.Server.Settlements
{
    public class SettlementBuilder
    {
        private readonly long reconciliationId;
        private readonly List<long> users;
        private readonly int userCount;
        private Dictionary<long, decimal> expensesPerUser;
        private List<ExpenseDTO> expenses;
        private decimal totalAmount;

        public SettlementBuilder(long reconciliationId, IEnumerable<UserDTO> users)
        {
            this.reconciliationId = reconciliationId;
            expenses = new List<ExpenseDTO>();
            expensesPerUser = new Dictionary<long, decimal>();
            this.users = users.Select(user => user.Id).ToList();

            userCount = this.users.Count;

            foreach(var user in users)
            {
                expensesPerUser.Add(user.Id, 0);
            }
        }

        public void AddExpense(ExpenseDTO expense)
        {
            expenses.Add(expense);
            totalAmount += expense.Amount;
            
            var payeeId = expense.PayeeId;

            expensesPerUser[payeeId] += expense.Amount;
        }

        public void AddExpenses(IEnumerable<ExpenseDTO> expenses)
        {
            foreach (ExpenseDTO e in expenses)
            {
                AddExpense(e);
            }
        }

        public IEnumerable<InsertSettlementCommand> Build()
        {
            List<InsertSettlementCommand> results = new List<InsertSettlementCommand>();

            foreach (long userId in users)
            {
                var spentAmount = expensesPerUser[userId];
                var outstanding = (totalAmount / userCount) - spentAmount;

                expensesPerUser[userId] = outstanding;
            }

            expensesPerUser = expensesPerUser.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            for (int i = 0; i < expensesPerUser.Count; i++)
            {
                var debtor = expensesPerUser.ElementAt(i).Key;
                if (expensesPerUser[debtor] > 0)
                {
                    for (int j = 0; j < expensesPerUser.Count; j++)
                    {
                        var debt = expensesPerUser[debtor];
                        var creditor = expensesPerUser.ElementAt(j).Key;
                        var credit = expensesPerUser[creditor];
                        if (credit < 0 && debt != 0)
                        {
                            decimal toPay = Math.Min(Math.Abs(credit), debt);
                            results.Add(new InsertSettlementCommand{
                                ReconciliationId = reconciliationId,
                                CreditorId = creditor,
                                DebtorId = debtor,
                                Amount = debt,
                                StatusId = 0
                            });
                            expensesPerUser[creditor] += toPay;
                            expensesPerUser[debtor] -= toPay;
                        }
                    }
                }
            }

            return results;
        }
    }
}