using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.DataBase
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var username = UsernameEntry.Text;
			var password = PasswordEntry.Text;

			Console.WriteLine("In Login");

			// Insérer ici le code pour vérifier les informations de connexion.
			// Cela pourrait être une demande à un service Web ou une vérification dans une base de données locale.

			try
			{
				// Check if user exist
				if (IsValidLogin(username, password))
				{
					// Naviguer vers la page de profil si la connexion est valide.
					// await Navigation.PushAsync(new AppShell());

					await ((App)Application.Current).NavigateToMainPage();
				}
				else
				{
					await DisplayAlert("Error", "Invalid username or password", "OK");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("44444444444444444444444444444444444444444444444444444444444444444444444444444");
				Console.WriteLine(ex.ToString());
				Console.WriteLine("44444444444444444444444444444444444444444444444444444444444444444444444444444");
			}
		}

		/*
		bool	IsValidLogin(string username, string password)
		{
			Console.WriteLine("Verify User: " + username);
			Console.WriteLine("Verify Pass: " + password);

			var dbConnection = new DatabaseConnection();
			return dbConnection.VerifyUser(username, password);
		}
		*/

		bool IsValidLogin(string username, string password)
		{
			// Insérer ici le code pour vérifier les informations de connexion.
			// Cette fonction est un exemple et doit être remplacée par une vérification réelle.

			return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
		}

		void OnCreateAccountButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new SignUpPage());
		}
	}
}