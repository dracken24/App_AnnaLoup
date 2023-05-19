using FindYourPath.Views;
using FindYourPath.DataBase;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace FindYourPath
{
	public partial class App : Application
	{
		static UserService userService;
		static string dbPath;

		public App()
		{
			InitializeComponent();

			dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Users.db3");

			// main page start program
			MainPage = new LoginPage();
		}

		public async Task NavigateToMainPage()
		{
			MainPage = new AppShell();
			await Shell.Current.GoToAsync("//main"); // Go to the main page after login
		}

		public static UserService UserService
		{
			get
			{
				if (userService == null)
				{
					userService = new UserService(dbPath);
				}
				return userService;
			}
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
