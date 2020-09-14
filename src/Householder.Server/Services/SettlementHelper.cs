using System.Linq;
using Householder.Server.Models;
using System.Collections.Generic;
using System;

namespace Householder.Server.Services
{
    public class SettlementBuilder
    {
        private readonly List<Resident> residents;
        private readonly int residentCount;
        private Dictionary<Resident, decimal> expensesPerResident;
        private List<Expense> expenses;
        private decimal totalAmount;

        public SettlementBuilder(List<Resident> residents)
        {
            expenses = new List<Expense>();
            expensesPerResident = new Dictionary<Resident, decimal>();
            this.residents = residents;

            residentCount = residents.Count;

            foreach(Resident r in residents)
            {
                expensesPerResident.Add(r, 0);
            }
        }

        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);
            totalAmount += expense.Amount;
            
            var resident = expense.Payee;

            expensesPerResident[resident] += expense.Amount;
        }

        public void AddExpenses(List<Expense> expenses)
        {
            foreach (Expense e in expenses)
            {
                AddExpense(e);
            }
        }

        public IEnumerable<Settlement> Build()
        {
            List<Settlement> results = new List<Settlement>();

            foreach (Resident resident in residents)
            {
                var spentAmount = expensesPerResident[resident];
                var outstanding = (totalAmount / residentCount) - spentAmount;

                expensesPerResident[resident] = outstanding;
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
                            results.Add(new Settlement(creditor, debtor, toPay, SettlementStatus.Pending));
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