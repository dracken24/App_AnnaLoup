using FindYourPath.Views.Finance;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views.Private
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTransactionPage : ContentPage
	{
		static FinancialPage _addTransactionPage;

		public AddTransactionPage(FinancialPage addTransactionPage)
		{
			InitializeComponent();

			_addTransactionPage = addTransactionPage;
		}

		private async void AddTransactionButton_Clicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(descriptionEntry.Text))
			{

				// Obtenir le type de transaction choisi par l'utilisateur
				string transactionType = (string)transactionTypePicker.SelectedItem;
				if (transactionType == "Dépense (Achat)")
				{
					if (double.TryParse(amountEntry.Text, out double amount))
					{
						// Si la conversion est réussie, créez une nouvelle instance de la classe Transaction avec les détails saisis
						Transaction newTransaction = new Transaction
						{
							Description = descriptionEntry.Text,
							Montant_Achat = (decimal)amount,
							Date_Achat = datePicker.Date,
							Type = TransactionType.Expense
						};
						Console.WriteLine("newTransaction: " + newTransaction.Description + " " + newTransaction.Montant_Achat + " " + newTransaction.Date_Achat);

						// Ajouter la transaction à la liste gérée par le FinanceManager
						App.FinanceManager.AddTransaction(newTransaction);

						// Mettez à jour l'affichage pour refléter les changements
						_addTransactionPage.UpdateUI();
						await App.FinanceService.SaveFinanceAsync(newTransaction);
					}
					else
					{
						// Afficher un message d'erreur si le montant saisi n'est pas valide
						await DisplayAlert("Erreur", "Montant non valide. Veuillez saisir un nombre.", "OK");
					}
				}
				else if (transactionType == "Entrée d'argent")
				{
					if (double.TryParse(amountEntry.Text, out double amount))
					{
						// Si la conversion est réussie, créez une nouvelle instance de la classe Transaction avec les détails saisis
						Transaction newTransaction = new Transaction
						{
							Description = descriptionEntry.Text,
							Montant_Achat = (decimal)amount,
							Date_Achat = datePicker.Date,
							Type = TransactionType.Income
						};
						Console.WriteLine("newTransaction: " + newTransaction.Description + " " + newTransaction.Montant_Achat + " " + newTransaction.Date_Achat);

						// Ajouter la transaction à la liste gérée par le FinanceManager
						App.FinanceManager.AddTransaction(newTransaction);

						// Mettez à jour l'affichage pour refléter les changements
						_addTransactionPage.UpdateUI();
						await App.FinanceService.SaveFinanceAsync(newTransaction);
					}
					else
					{
						// Afficher un message d'erreur si le montant saisi n'est pas valide
						await DisplayAlert("Erreur", "Montant non valide. Veuillez saisir un nombre.", "OK");
					}
				}
			}

			// Fermer la page
			await Navigation.PopAsync();
		}
	}
}