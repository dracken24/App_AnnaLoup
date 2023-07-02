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

		public Resources()
		{
			InitializeComponent();

			Title = "Resources";

			App.ResourcesCollection = new ObservableCollection<OneRessource>();
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

		private void OnAddResourceButtonClicked(object sender, System.EventArgs e)
		{
			OneRessource newResource = new OneRessource
			{
				Nom = nameEntry.Text,
				Adresse = addressEntry.Text,
				Telephone = phoneEntry.Text,
				URL = urlEntry.Text,
				Description = descriptionEntry.Text,
				Type = type.Text,
			};

			App.ResourcesCollection.Add(newResource);

			// Clear the entries
			nameEntry.Text = string.Empty;
			addressEntry.Text = string.Empty;
			phoneEntry.Text = string.Empty;
			urlEntry.Text = string.Empty;
			type.Text = string.Empty;
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var resource = (OneRessource)e.Item;
			await Navigation.PushAsync(new PrivateOneRessourceDetailPage(resource));
		}
	}
}
