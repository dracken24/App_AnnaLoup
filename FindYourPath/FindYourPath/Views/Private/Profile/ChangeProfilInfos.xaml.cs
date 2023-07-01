using FindYourPath.DataBase;
using Newtonsoft.Json.Linq;
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
		private UserService _userService;

		public ChangeProfilInfos(ProfileViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			this.BindingContext = _viewModel;
			_userService = new UserService(App.ConnectionString);
		}

		async void OnSubmitButtonClicked(object sender, EventArgs e)
		{
			App.User._surname = _viewModel.FirstName;
			App.User._name = _viewModel.Name;
			App.User._adress = _viewModel.Address;
			App.User._email = _viewModel.Email;
			App.User._phone = _viewModel.Phone;

			JObject paramJson = new JObject
			{
				["action"] = "modifAccount",
				["id"] = App.User._id,
				["email"] = _viewModel.Email,
				["lastName"] = _viewModel.Name,
				["firstName"] = _viewModel.FirstName,
				["phone"] = _viewModel.Phone,
				["address"] = _viewModel.Address,
			};

			try
			{
				if (await _userService.UpdateUser(paramJson))
				{
					// Display a success message to the user
					await DisplayAlert("Profile Updated", "Your profile has been updated.", "OK");
				}
				else
				{
					// Display a success message to the user
					await DisplayAlert("Profile Updated", "Une erreur est subvenue lors de la sauvegarde du profil.", "OK");
				}
			}
			catch (Exception ex)
			{
				// Display an error message to the user
				Console.WriteLine("*** Erreur lors de la sauvegarde du profil ex:" + ex.Message);
				await DisplayAlert("Profile Updated", "Une erreur est subvenue lors de la sauvegarde du profil.", "OK");
			}	
			
			// Navigate back to the previous page (the Profile page)
			await Navigation.PopAsync();
		}
	}
}
