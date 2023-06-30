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
		private ProfileViewModel _viewModel;

		public ChangeProfilInfos(ProfileViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			this.BindingContext = _viewModel;
		}

		async void OnSubmitButtonClicked(object sender, EventArgs e)
		{
			App.User._name = _viewModel.Name;
			App.User._adress = _viewModel.Address;
			App.User._email = _viewModel.Email;
			App.User._phone = _viewModel.Phone;

			// Display a success message to the user
			await DisplayAlert("Profile Updated", "Your profile has been updated.", "OK");
			/*Console.WriteLine("NAME: " + App.User._name);
			Console.WriteLine("ADDRESS: " + App.User._adress);
			Console.WriteLine("EMAIL: " + _viewModel.Email);
			Console.WriteLine("PHONE: " + _viewModel.Phone);*/

			// Navigate back to the previous page (the Profile page)
			await Navigation.PopAsync();
		}
	}
}
