using FindYourPath.Services;
using FindYourPath.Views.Finance;
using FindYourPath.Views.Private;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FinancialPage : ContentPage
	{
		public FinancialPage()
		{
			InitializeComponent();

			GetListFromDatabase();
		}

		public async Task GetListFromDatabase()
		{
			await App.FinanceService.GetRessourcesAsync(App.User._id);

			foreach (Transaction transaction in App.Transactions)
			{
				Console.WriteLine("-*-*- Transaction Description: " + transaction.Description + " Amount: " + transaction.Montant_Achat + " Date: " + transaction.Date_Achat + " Type: " + transaction.Type);
			}

			// Liez la liste de transactions au ListView
			transactionsListView.ItemsSource = App.Transactions;
			UpdateUI();
		}

		private async void OnTransactionSelected(object sender, SelectedItemChangedEventArgs e)
		{
			// Vérifiez si l'élément sélectionné est une transaction (et non null)
			if (e.SelectedItem is Transaction selectedTransaction)
			{
				Console.WriteLine("--- SelectedTransaction: " + selectedTransaction.Description + " " + selectedTransaction.Montant_Achat + " " + selectedTransaction.Date_Achat);
				await Navigation.PushAsync(new ModifyTransaction(this, selectedTransaction));
			}

			// Désélectionner l'élément sélectionné pour permettre de le sélectionner à nouveau ultérieurement
			transactionsListView.SelectedItem = null;
		}

		private async void AddTransactionButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTransactionPage(this));
		}

		public void UpdateUI()
		{
			// Mettez à jour les contrôles d'interface utilisateur pour afficher les informations financières
			balanceLabel.Text = App.FinanceManager.GetBalance().ToString("C");
			totalIncomeLabel.Text = App.FinanceManager.GetTotalIncome().ToString("C");
			totalExpensesLabel.Text = App.FinanceManager.GetTotalExpenses().ToString("C");

			transactionsListView.ItemsSource = null;
			transactionsListView.ItemsSource = App.Transactions;
		}
	}
}

