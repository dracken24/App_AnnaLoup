using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using FindYourPath.DataBase;
using FindYourPath.Services;
using FindYourPath.Views.Private.Ressources;

namespace FindYourPath.Views.Private
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrivateOneRessourceDetailPage : ContentPage
	{
		OneRessource _oneRessource;
		RessourceService _ressourceService;
		public PrivateOneRessourceDetailPage(OneRessource resource, RessourceService ressourceService)
		{
			InitializeComponent();
			
			_oneRessource = resource;
			_ressourceService = ressourceService;
			Title = resource.Nom;

			nameLabel.Text = $"Nom             :";
			addressLabel.Text = $"Adresse      :";
			phoneLabel.Text = $"Téléphone  :";
			urlLabel.Text = $"Url                 :";
			descriptionLabel.Text = $"Description :";

			nameDescription.Text = resource.Nom;
			addressDescription.Text = resource.Adresse;
			phoneDescription.Text = resource.Telephone;
			urlDescription.Text = resource.URL;
			descriptionDescription.Text = resource.Description;
		}

		async void OnUrlTapped(object sender, EventArgs e)
		{
			var url = urlDescription.Text;
			if (!string.IsNullOrWhiteSpace(url))
			{
				await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
			}
		}

/*
		async void OnPhoneTapped(object sender, EventArgs e)
		{
			var phone = phoneDescription.Text;
			if (!string.IsNullOrWhiteSpace(phone))
			{
				await Launcher.OpenAsync($"tel:{phone}");
			}
		}
*/

		private async void OnModifyButtonClicked(object sender, EventArgs e)
		{
			Console.WriteLine("Modify event");

			User user = App.User;
			try
			{
				Navigation.PushAsync(new ContactModifier(_oneRessource, _ressourceService, user));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			// Then navigate back to the previous page
			//Navigation.PopAsync();
		}
	}
}
