using FindYourPath.Views;
using FindYourPath.Services;
using FindYourPath.DataBase;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using XCalendar.Core.Collections;
using System.Collections.ObjectModel;

namespace FindYourPath
{
	public partial class App : Application
	{
		static UserService _userService; // Pour la gestion des connexions la Database
		static RessourceService _ressourceService; // Pour la gestion des contactes de la Database
		static string _connectionString; // String de connexion a la Database
		static User _user = null; // User principale

		static ObservableRangeCollection<MyEvent> _Events = new ObservableRangeCollection<MyEvent>();
		static ObservableCollection<OneRessource> _resources = new ObservableCollection<OneRessource>();

		public App()
		{
			InitializeComponent();
			
			_connectionString = "http://dracken24.duckdns.org/apiAnnaLoup/actions";
			_userService = new UserService(_connectionString);
			_ressourceService = new RessourceService(_connectionString);

			MainPage = new NavigationPage(new LoginPage());
		}

		public static ObservableRangeCollection<MyEvent> Events
		{
			get { return _Events; }
			set { _Events = value; }
		}

		public static ObservableCollection<OneRessource> ResourcesCollection
		{
			get { return _resources; }
			set { _resources = value; }
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

		public static RessourceService RessourceService
		{
			get { return _ressourceService; }
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



/*
La communication directe entre une application mobile et une base de données n'est
généralement pas recommandée pour plusieurs raisons. Voici quelques-unes de ces raisons :

Sécurité : Les informations d'authentification à la base de données ne doivent jamais
être stockées sur le dispositif de l'utilisateur. Si ces informations étaient découvertes,
cela pourrait entraîner une compromission totale de votre base de données.

Performance : Les bases de données sont optimisées pour fonctionner sur des réseaux locaux,
et non sur Internet. Les performances peuvent donc être nettement inférieures si votre
application mobile se connecte directement à la base de données.

Compatibilité : De nombreux types de bases de données ne disposent pas de pilotes compatibles
avec les systèmes d'exploitation mobiles. Il pourrait donc être impossible d'établir une
connexion directe de toute façon.

Evolutivité : Si votre application doit être mise à l'échelle, il est préférable de mettre
en place une architecture serveur qui peut être mise à l'échelle indépendamment de votre
base de données.

En raison de ces contraintes, le schéma typique consiste à avoir une application serveur
qui expose une API REST ou GraphQL que votre application mobile peut utiliser pour communiquer
avec la base de données. Votre application serveur peut être écrite en PHP, Node.js, Ruby,
Python, Java, .NET, ou tout autre langage qui peut recevoir des requêtes HTTP, se connecter
à une base de données, et renvoyer des réponses HTTP.

Dans votre cas, vous avez déjà une application PHP qui fait cela. Pour communiquer
directement avec la base de données, vous auriez à mettre en œuvre la logique de votre
application PHP dans votre application mobile, ce qui n'est généralement pas recommandé
pour les raisons énumérées ci-dessus.

En fin de compte, il est plus sûr, plus performant et plus évolutif de continuer à utiliser
votre API PHP pour interagir avec votre base de données.
 */
