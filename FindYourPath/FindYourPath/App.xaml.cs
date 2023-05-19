using FindYourPath.Views;
using FindYourPath.DataBase;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
			// MainPage = new LoginPage();
			MainPage = new NavigationPage(new LoginPage());
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
