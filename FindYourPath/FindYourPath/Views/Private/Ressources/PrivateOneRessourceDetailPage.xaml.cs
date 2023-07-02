using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace FindYourPath.Views.Private
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrivateOneRessourceDetailPage : ContentPage
	{
		OneRessource _oneRessource;
		public PrivateOneRessourceDetailPage(OneRessource resource)
		{
			InitializeComponent();
			
			_oneRessource = resource;
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

		async void OnAddToPrivateButtonTapped(object sender, EventArgs e)
		{
			await App.RessourceService.SaveRessourceAsync(_oneRessource);
			await Navigation.PopAsync();
		}
	}
}
