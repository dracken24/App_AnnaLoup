using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.DataBase
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage()
		{
			InitializeComponent();
		}

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			var username = UsernameEntry.Text;
			var email = EmailEntry.Text;
			var password = PasswordEntry.Text;

			if (IsValidRegistration(username, email, password))
			{
				try
				{
					// Create the new user
					var userService = new UserService(App.DatabasePath);
					await userService.AddUser(username, email, password); // use 'await' here
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					await DisplayAlert("Error", "Invalid connexion", "OK");
					return;
				}

				Console.WriteLine("222");

				await ((App)Application.Current).NavigateToMainPage();
			}
			else
			{
				await DisplayAlert("Error", "Invalid registration information", "OK");
			}
		}

		bool IsValidRegistration(string username, string email, string password)
		{
			// Insérer ici le code pour vérifier les informations d'inscription.
			// Cette fonction est un exemple et doit être remplacée par une vérification réelle.

			return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email);
		}

		void OnBackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}