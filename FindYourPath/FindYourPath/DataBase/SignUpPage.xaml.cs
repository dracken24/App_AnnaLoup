﻿using System;
using System.Net.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FindYourPath.RightGestion;
using GoogleApi.Entities.Maps.AerialView.Common;

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

			var date = DateTime.Today.Date;
			var birthDateTime = new DateTime(date.Year, date.Month, date.Day);

			JObject paramJson = new JObject
			{
				["username"] = username,
				["email"] = email,
				["password"] = password,
				["lastName"] = "",
				["firstName"] = "",
				["phone"] = "",
				["birthDate"] = birthDateTime.Date,
				["sexe"] = "",
				["address"] = "",
			};

			try
			{
				if (IsValidRegistration(username, email, password))
				{
					// Use the UserService from the App class instead of creating a new one
					var userService = App.UserService;
					await userService.AddUser(paramJson); // use 'await' here
				}
				else
				{
					await DisplayAlert("Error", "Invalid registration information", "OK");
				}
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine("Une erreur s'est produite lors de la connexion au serveur : " + ex);
				await DisplayAlert("Erreur", "Impossible de se connecter au serveur. " + ex.Message, "OK");
				return;
			}
			catch (JsonException ex)
			{
				Console.WriteLine("Une erreur s'est produite lors de l'analyse de la réponse du serveur : " + ex);
				await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. " + ex.Message, "OK");
				return;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Une erreur inattendue s'est produite : " + ex);
				await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. " + ex.Message, "OK");
				return;
			}

			// Console.WriteLine("222");
			await Navigation.PushAsync(new ConfirmEmail());
		}

		bool IsValidRegistration(string username, string email, string password)
		{
			// TODO: Insérer ici le code pour vérifier les informations d'inscription.
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
			{
				throw new Exception("Un ou plusieurs champs sont vides.");
			}
			else if (password.Length < 8 || password.Length > 24)
			{
				throw new Exception("Le mot de passe doit contenir plus de 8 ou moins de 24 characters.");
			}
			else if (!email.Contains("@"))
			{
				throw new Exception("L'adresse courriel n'est pas valide.");
			}
			/*else if (username.Length < 4 || username.Length > 24)
			{
				throw new Exception("Le nom d'utilisateur doit contenir plus de 4 ou moins de 24 characters.");
			}*/

			Console.WriteLine("GOOD infos.\n");
			return true;
		}
	}
}