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

			// Insérer ici le code pour créer le compte utilisateur.
			// Cela pourrait être une demande à un service Web ou une insertion dans une base de données locale.

			if (IsValidRegistration(username, email, password))
			{
				// Créer le nouvel utilisateur
				var newUser = new User
				{
					Username = username,
					Email = email,
					// Dans une vraie situation, vous ne devez pas stocker le mot de passe en texte clair.
					// Assurez-vous d'utiliser un hachage sécurisé pour le stockage des mots de passe.
				};
				await App.UserService.SaveUserAsync(newUser);

				// Naviguer vers la page de profil après l'inscription.
				await Navigation.PushAsync(new MainPage());
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
	}
}