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
		static UserService _userService;
		static string _connectionString;

		public App()
		{
			InitializeComponent();

			_connectionString = "http://dracken24.duckdns.org/apiAnnaLoup/actions/connectUser.php";
			// _connectionString = "server=https://10.0.2.2/apiAnnaLoup/actions/connectUser.php;user=root;password=Serpents24.;database=users";
			_userService = new UserService(_connectionString);

			MainPage = new LoginPage();
		}

		public static string ConnectionString
		{
			get { return _connectionString; }
		}

		public async Task NavigateToMainPage()
		{
			MainPage = new AppShell();
			await Shell.Current.GoToAsync("//main");
		}

		public static UserService UserService
		{
			get { return _userService; }
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

// TODO: enlever "android:usesCleartextTraffic="true"" du fichier AndroidManifest.xml 'http'
// avant deploiement. Requiert: SSL sertificat pour htpps
