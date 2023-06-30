using FindYourPath.DataBase;
using FindYourPath.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.RightGestion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmEmail : ContentPage
	{
		// Entry verificationCodeEntry;
		private static HttpClient _httpClient = new HttpClient();

		public ConfirmEmail()
		{
			InitializeComponent();

			verificationCodeEntry = new Entry
			{
				Placeholder = "Enter verification code"
			};

			Button submitButton = new Button
			{
				Text = "Submit"
			};

			submitButton.Clicked += OnSubmitButtonClicked;

			Content = new StackLayout
			{
				Children = { verificationCodeEntry, submitButton }
			};
		}

		private async void OnSubmitButtonClicked(object sender, EventArgs e)
		{
			var verificationCode = verificationCodeEntry.Text;

			// Call your API to check the verification code
			var isCodeCorrect = App.User.CheckVerificationCode(verificationCode);

			if (isCodeCorrect)
			{
				// If the code is correct, navigate to the next page
				await ChangeConnectRight();
				await ((App)Application.Current).NavigateToMainPage();
			}
			else
			{
				// If the code is incorrect, display an error message
				await DisplayAlert("Error", "The verification code is incorrect.", "OK");
			}
		}

		private async Task ChangeConnectRight()
		{
			JObject paramJson = new JObject
			{
				["action"] = "verifEmail",
				["email_verif"] = true,
				["user_id"] = App.User._id
			};
			Console.WriteLine("--- user_id: " + App.User._id);

			var content = new StringContent(JsonConvert.SerializeObject(paramJson), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(App.ConnectionString + "/verifyAccount.php", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse));
				if (result.ContainsKey("success") && (bool)result["success"])
				{
					App.User.SetCanConnect(true);
					Console.WriteLine("*** USER ***: " + result["user"]); // infos du user
				}
				else
				{
					// Optionally, log the error message if present
					if (result.ContainsKey("error"))
					{
						Console.WriteLine("Error from server: " + result["error"]);
					}
				}
			}
			else
			{
				var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse));
				Console.WriteLine("Error from server: " + result["error"]);
			}
		}
	}
}
