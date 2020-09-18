using System.Linq;
using Householder.Server.Models;
using System.Collections.Generic;
using System;

namespace Householder.Server.Settlements
{
    public class SettlementBuilder
    {
        private readonly long reconciliationId;
        private readonly List<long> residents;
        private readonly int residentCount;
        private Dictionary<long, decimal> expensesPerResident;
        private List<Expense> expenses;
        private decimal totalAmount;

        public SettlementBuilder(long reconciliationId, IEnumerable<Resident> residents)
        {
            this.reconciliationId = reconciliationId;
            expenses = new List<Expense>();
            expensesPerResident = new Dictionary<long, decimal>();
            this.residents = residents.Select(r => r.Id).ToList();

            residentCount = this.residents.Count;

            foreach(Resident r in residents)
            {
                expensesPerResident.Add(r.Id, 0);
            }
        }

        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);
            totalAmount += expense.Amount;
            
            var residentId = expense.ResidentId;

            expensesPerResident[residentId] += expense.Amount;
        }

        public void AddExpenses(IEnumerable<Expense> expenses)
        {
            foreach (Expense e in expenses)
            {
                AddExpense(e);
            }
        }

        public IEnumerable<InsertSettlementCommand> Build()
        {
            List<InsertSettlementCommand> results = new List<InsertSettlementCommand>();

            foreach (long residentId in residents)
            {
                var spentAmount = expensesPerResident[residentId];
                var outstanding = (totalAmount / residentCount) - spentAmount;

                expensesPerResident[residentId] = outstanding;
            }

            expensesPerResident = expensesPerResident.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            for (int i = 0; i < expensesPerResident.Count; i++)
            {
                var debtor = expensesPerResident.ElementAt(i).Key;
                if (expensesPerResident[debtor] > 0)
                {
                    for (int j = 0; j < expensesPerResident.Count; j++)
                    {
                        var debt = expensesPerResident[debtor];
                        var creditor = expensesPerResident.ElementAt(j).Key;
                        var credit = expensesPerResident[creditor];
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
                            expensesPerResident[creditor] += toPay;
                            expensesPerResident[debtor] -= toPay;
                        }
                    }
                }
            }

            return results;
        }
    }
}