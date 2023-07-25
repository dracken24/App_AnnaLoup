using FindYourPath.DataBase;
using FindYourPath.Services;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views.Private.Ressources
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactModifier : ContentPage
	{
		private RessourceService _ressourceService;
		private OneRessource _ressource;
		static User _user;

		public ContactModifier (OneRessource appointments, RessourceService ressourceService, User user)
		{
			InitializeComponent ();
			_ressourceService = ressourceService;
			_ressource = appointments;

			nomEntry.Text = appointments.Nom;
			adressePicker.Text = appointments.Adresse;
			phonePicker.Text = appointments.Telephone;
			urlPicker.Text = appointments.URL;
			typePicker.Text = appointments.Type;
			descriptionPicker.Text = appointments.Description;

			_user = user;
		}

		private async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			// Here you would save your event to your data source
			// and then navigate back to the calendar page
			Console.WriteLine("Save Modify event");

			OneRessource ressourceModifier = new OneRessource
			{
				id = _ressource.id,
				Nom = nomEntry.Text,
				Adresse = adressePicker.Text,
				Telephone = phonePicker.Text,
				URL = urlPicker.Text,
				Type = typePicker.Text,
				Description = descriptionPicker.Text,
				User_Id = _user._id
			};
			_ressource = ressourceModifier;
			await _ressourceService.UpdateEventAsync(_ressource);
			await App.Instance.RefreshResources();
			// Then navigate back to the previous page
			await Navigation.PopToRootAsync();
		}

		private async void OnDeleteButtonClicked(object sender, EventArgs e)
		{
			Console.WriteLine("Delete event");
			var confirmResult = await DisplayAlert("Confirmation", "Are you sure you want to delete this contact?", "Yes", "No");
			if (confirmResult)
			{
				// Supprimez l'événement de la base de données
				var deleteResult = await _ressourceService.DeleteEventAsync(_ressource);

				if (deleteResult)
				{
					// Remove event from local list
					App.ResourcesCollection.Remove(_ressource);

					// Puis revenez à la page précédente
					await Navigation.PopToRootAsync();
				}
				else
				{
					await DisplayAlert("Error", "Failed to delete event.", "OK");
				}
			}
		}
	}
}