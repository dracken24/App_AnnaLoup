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

			try
			{
				// Check if user exist
				if (await IsValidLogin(username, password))  // utilise maintenant await
				{
					await ((App)Application.Current).NavigateToMainPage();
				}
				else
				{
					await DisplayAlert("Error", "Invalid username or password", "OK");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

	
		async Task<bool> IsValidLogin(string username, string password)
		{
			Console.WriteLine("Verify User: " + username);
			Console.WriteLine("Verify Pass: " + password);

			var userService = new UserService(App.DatabasePath);
			return await userService.VerifyUserAsync(username, password);
		}

/*		
		async Task<bool> IsValidLogin(string username, string password)
		{
			// doit être remplacée par une vérification réelle.

			return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
		}
*/		

		void OnCreateAccountButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new SignUpPage());
		}
	}
}
