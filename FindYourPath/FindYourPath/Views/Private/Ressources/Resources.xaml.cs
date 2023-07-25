using FindYourPath.Services;
using FindYourPath.Views.Private;
using FindYourPath.Views.Public.RessourcesPublic;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FindYourPath.Views
{
	public partial class Resources : ContentPage
	{
		// private static ObservableCollection<OneRessource> _resources;
		RessourceService _ressourceService;

		public Resources()
		{
			InitializeComponent();

			Title = "Resources";

			App.ResourcesCollection = new ObservableCollection<OneRessource>();
			_ressourceService = new RessourceService(App.ConnectionString);
			resourceList.ItemsSource = App.ResourcesCollection;

			TakeRessourcesFromDatabase();
		}

		private async void TakeRessourcesFromDatabase()
		{
			try
			{
				var resources = await App.RessourceService.GetRessourcesAsync(App.User._id);
				foreach (var resource in resources)
				{
					App.ResourcesCollection.Add(resource);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error message: " + ex.Message);
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}

		private async void OnAddResourceButtonClicked(object sender, System.EventArgs e)
		{
			OneRessource newResource = new OneRessource
			{
				Nom = nameEntry.Text,
				Adresse = addressEntry.Text,
				Telephone = phoneEntry.Text,
				URL = urlEntry.Text,
				Type = type.Text,
				Description = descriptionEntry.Text,
				User_Id = App.User._id
			};

			App.ResourcesCollection.Add(newResource);
			await App.RessourceService.SaveRessourceAsync(newResource);

			// Clear the entries
			nameEntry.Text = string.Empty;
			addressEntry.Text = string.Empty;
			phoneEntry.Text = string.Empty;
			urlEntry.Text = string.Empty;
			type.Text = string.Empty;
			descriptionEntry.Text = string.Empty;
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var resource = (OneRessource)e.Item;
			await Navigation.PushAsync(new PrivateOneRessourceDetailPage(resource, _ressourceService));
		}
	}
}
