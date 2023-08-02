using System;
using System.Globalization;

namespace FindYourPath.Views.Finance
{
	public class Transaction
	{
		public int id { get; set; }
		public string Description { get; set; }
		public DateTime Date_Achat { get; set; }
		public decimal Montant_Achat { get; set; }
		public TransactionType Type { get; set; }
		// Vous pouvez ajouter d'autres propriétés comme la catégorie, le compte associé, etc.
		public int UserId { get; set; }
	}

	public enum TransactionType
	{
		Expense,
		Income,
		Transfer,
		Init
	}

}
