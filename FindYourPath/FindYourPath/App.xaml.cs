using FindYourPath.Views;
using FindYourPath.DataBase;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using GoogleApi.Entities.Maps.StreetView.Request.Enums;

namespace FindYourPath
{
	public partial class App : Application
	{
		static UserService userService;
		static string _databasePath;

		public App()
		{
			InitializeComponent();

			var dbName = "MyDb.db3";
			#if __IOS__
				string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");
				_databasePath = Path.Combine(folderPath, dbName);
			#else
				// This will work for Android and UWP.
				_databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
			#endif

			_databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Users.db3");

			// main page start program
			MainPage = new LoginPage();
		}

		public static string DatabasePath
		{
			get
			{
				if (_databasePath == null)
				{
					var dbName = "MyDb.db3";
					#if __IOS__
					string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");
					_databasePath = Path.Combine(folderPath, dbName);
					#else
					// This will work for Android and UWP.
					_databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
					#endif
				}
				return _databasePath;
			}
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
					userService = new UserService(_databasePath);
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
