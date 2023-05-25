using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views.Private.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangeProfilInfos : ContentPage
	{
		private ProfileViewModel viewModel;

		public ChangeProfilInfos(ProfileViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;

			nameEntry.SetBinding(Entry.TextProperty, new Binding("Name", source: viewModel));
			adresseEntry.SetBinding(Entry.TextProperty, new Binding("Address", source: viewModel));
			emailEntry.SetBinding(Entry.TextProperty, new Binding("Email", source: viewModel));
			phoneEntry.SetBinding(Entry.TextProperty, new Binding("Phone", source: viewModel));
		}

		async void OnSubmitButtonClicked(object sender, EventArgs e)
		{
			// Display a success message to the user
			await DisplayAlert("Profile Updated", "Your profile has been updated.", "OK");

			// Navigate back to the previous page (the Profile page)
			await Navigation.PopAsync();
		}
	}
}