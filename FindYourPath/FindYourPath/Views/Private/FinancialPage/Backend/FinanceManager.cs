using System.Collections.Generic;
using System.Linq;

namespace FindYourPath.Views.Finance
{
	public class FinanceManager
	{
		public FinanceManager()
		{

		}

		public void AddTransaction(Transaction transaction)
		{
			App.Transactions.Add(transaction);
		}

		public decimal GetBalance()
		{
			return GetTotalIncome() - GetTotalExpenses();
		}

		public decimal GetTotalIncome()
		{
			return App.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Montant_Achat);
		}

		public decimal GetTotalExpenses()
		{
			return App.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Montant_Achat);
		}

		public List<Transaction> GetTransactions()
		{
			return App.Transactions;
		}

		// Ajoutez d'autres méthodes pour les statistiques, les budgets, etc.
	}

}
