using FindYourPath.Views.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views.Private
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifyTransaction : ContentPage
	{
		static FinancialPage _addTransactionPage;
		static Transaction _modifTransaction;

		public ModifyTransaction(FinancialPage addTransactionPage, Transaction modifTransaction)
		{
			InitializeComponent();


			descriptionEntry.Text  = modifTransaction.Description;
			amountEntry.Text = modifTransaction.Montant_Achat.ToString("F2");
			datePicker.Date = modifTransaction.Date_Achat;

			transactionTypePicker.ItemsSource = Enum.GetValues(typeof(TransactionType));
			transactionTypePicker.SelectedItem = modifTransaction.Type;
			//transactionTypePicker.TabIndex = (int)modifTransaction.Type;

			_modifTransaction = modifTransaction;
			_addTransactionPage = addTransactionPage;
		}

		private async void OnModifyClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(descriptionEntry.Text))
			{

				// Obtenir le type de transaction choisi par l'utilisateur
				string transactionType = (string)transactionTypePicker.SelectedItem;
				if (double.TryParse(amountEntry.Text, out double amount))
				{
					// Si la conversion est réussie, créez une nouvelle instance de la classe Transaction avec les détails saisis
					Transaction newTransaction = new Transaction
					{
						id = _modifTransaction.id,
						Description = descriptionEntry.Text,
						Montant_Achat = (decimal)amount,
						Date_Achat = datePicker.Date,
						Type = _modifTransaction.Type,
						UserId = _modifTransaction.UserId
					};
					Console.WriteLine("newTransaction: " + newTransaction.Description + " " + newTransaction.Montant_Achat + " " + newTransaction.Date_Achat);

					// Ajouter la transaction à la liste gérée par le FinanceManager
					App.FinanceManager.UpdateTransaction(newTransaction);

					// Mettez à jour l'affichage pour refléter les changements
					_addTransactionPage.UpdateUI();
					await App.FinanceService.ModifyFinanceAsync(newTransaction);
				}
				else
				{
					// Afficher un message d'erreur si le montant saisi n'est pas valide
					await DisplayAlert("Erreur", "Montant non valide. Veuillez saisir un nombre.", "OK");
				}
			}

			// Fermer la page
			await Navigation.PopAsync();
		}

		private async void OnDeleteClicked(object sender, EventArgs e)
		{
			Console.WriteLine("Delete Transaction");
			var confirmResult = await DisplayAlert("Confirmation", "Are you sure you want to delete this Transaction?", "Yes", "No");
			if (confirmResult)
			{
				// Supprimez l'événement de la base de données
				var deleteResult = await App.FinanceService.DeleteFinanceAsync(_modifTransaction); ;

				if (deleteResult)
				{
					// Supprimer la transaction de la liste gérée par le FinanceManager
					App.FinanceManager.DeleteTransaction(_modifTransaction);

					// Mettez à jour l'affichage pour refléter les changements
					_addTransactionPage.UpdateUI();
					//await App.FinanceService.DeleteFinanceAsync(_modifTransaction);

					// Fermer la page
					await Navigation.PopAsync();
					// Puis revenez à la page précédente
					// await Navigation.PopToRootAsync();
				}
				else
				{
					await DisplayAlert("Error", "Failed to delete event.", "OK");
				}
			}			
		}
	}
}