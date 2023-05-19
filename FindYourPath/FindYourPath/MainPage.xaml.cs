using FindYourPath.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindYourPath
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			// Ajoutez ici toutes les pages que vous voulez dans vos onglets.
			Children.Add(new Welcome { Title = "General" });
			Children.Add(new Agenda { Title = "Agenda" });
			Children.Add(new TodoList { Title = "Todo List" });
			Children.Add(new Resources { Title = "Ressources" });
			Children.Add(new Profile { Title = "Profile" });

			// Cacher la barre de navigation pour cette page
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
