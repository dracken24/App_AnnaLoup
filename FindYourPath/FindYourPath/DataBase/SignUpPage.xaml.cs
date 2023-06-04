using System;
using System.Net.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

			JObject paramJson = new JObject
			{
				["username"] = username,
				["email"] = email,
				["password"] = password
			};

			if (IsValidRegistration(username, email, password))
			{
				try
				{
					// Use the UserService from the App class instead of creating a new one
					var userService = App.UserService;
					await userService.AddUser(paramJson); // use 'await' here
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine("Une erreur s'est produite lors de la connexion au serveur : " + ex);
					await DisplayAlert("Erreur", "Impossible de se connecter au serveur. Veuillez vérifier votre connexion internet.", "OK");
					return;
				}
				catch (JsonException ex)
				{
					Console.WriteLine("Une erreur s'est produite lors de l'analyse de la réponse du serveur : " + ex);
					await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. Veuillez réessayer plus tard.", "OK");
					return;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Une erreur inattendue s'est produite : " + ex);
					await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. Veuillez réessayer plus tard.", "OK");
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
			// TODO: Insérer ici le code pour vérifier les informations d'inscription.
			// Cette fonction est un exemple et doit être remplacée par une vérification réelle.

			return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email);
		}

		void OnBackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}