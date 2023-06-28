using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace FindYourPath.DataBase
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			BindingContext = new LoginPageViewModel();
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var username = ((LoginPageViewModel)BindingContext).Username;
			var password = ((LoginPageViewModel)BindingContext).Password;

			JObject paramJson = new JObject
			{
				["username"] = username,
				["password"] = password
			};

			// TODO: Remove these debug lines
			// Console.WriteLine("In Login");
			// Console.WriteLine("Username: " + username);
			// Console.WriteLine("Password: " + password);

			try
			{
				// Check if user exist
				if (await IsValidLogin(paramJson))
				{
					//App.Print
					await ((App)Application.Current).NavigateToMainPage();
				}
				else
				{
					await DisplayAlert("Error", "Invalid username or password", "OK");
				}
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine("Une erreur s'est produite lors de la connexion au serveur : " + ex);
				await DisplayAlert("Erreur", "Impossible de se connecter au serveur.", "OK");
			}
			catch (JsonException ex)
			{
				Console.WriteLine("Une erreur s'est produite lors de l'analyse de la réponse du serveur : " + ex);
				await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. " + ex.Message, "OK");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Une erreur inattendue s'est produite : " + ex);
				await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. " + ex.Message, "OK");
			}
		}

		async Task<bool> IsValidLogin(JObject paramJson)
		{
			Console.WriteLine("Verify paramJson: " + paramJson.ToString());

			var userService = App.UserService;
			return await userService.ConnectUser(paramJson);
		}

		void OnCreateAccountButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new SignUpPage());
		}
	}
}
