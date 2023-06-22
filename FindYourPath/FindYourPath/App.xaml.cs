using FindYourPath.Views;
using FindYourPath.DataBase;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using XCalendar.Core.Collections;

namespace FindYourPath
{
	public partial class App : Application
	{
		static UserService _userService; // Pour la gestion de la Database
		static string _connectionString; // String de connexion a la Database
		static User _user = null; // User principale

		static ObservableRangeCollection<Event> _Events = new ObservableRangeCollection<Event>();

		public App()
		{
			InitializeComponent();
			// _connectionString = "http://174.91.185.220/apiAnnaLoup/actions";
			_connectionString = "http://dracken24.duckdns.org/apiAnnaLoup/actions";
			_userService = new UserService(_connectionString);

			MainPage = new LoginPage();
		}

		public static ObservableRangeCollection<Event> Events
		{
			get { return _Events; }
			set { _Events = value; }
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

		internal static User User
		{
			get { return _user; }
		}
		public static void SaveUser(object user)
		{
			User user1 = new User();
			user1.SaveUser(user);
			_user = user1;
		}

		// TODO: Remove, PrintMembers() est pour le debug
		public static void PrintMembers()
		{
			_user.PrintMembers();
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

/*
 * Sauvegarde des données : Assurez-vous de sauvegarder toutes vos bases de données et fichiers de site Web. Pour les bases de données MySQL, vous pouvez utiliser la commande mysqldump pour créer une sauvegarde.

Installation de LAMP : Installez un environnement LAMP sur votre serveur Linux. Cela comprend généralement l'installation de Linux, Apache, MySQL et PHP.

Restauration des données : Après avoir installé LAMP, vous pouvez restaurer vos bases de données en utilisant la sauvegarde que vous avez créée. Les fichiers de site Web peuvent être téléchargés dans le répertoire approprié (généralement /var/www/).

Configuration : Vous devrez peut-être également configurer Apache et PHP pour qu'ils correspondent à la configuration de votre ancien serveur WAMP. Cela peut inclure la modification des fichiers .htaccess, l'installation de modules PHP supplémentaires, etc.

Test : Enfin, testez soigneusement votre site pour vous assurer qu'il fonctionne correctement sur le nouveau serveur LAMP.
 * */
