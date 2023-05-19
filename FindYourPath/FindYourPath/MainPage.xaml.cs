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
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		// On click, go to PersonnalInfos page
		private void ProfileClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new PersonnalInfos());
        }

		// On click, go to Agenda page
		private void AgendaClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new Agenda());
		}

		// On click, go to TodoList page
		private void TodoListClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new TodoList());
		}

		// On click, go to Encore page
		private void EncoreClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new Encore());
		}

		// On click, go to Ressources page
		private void RessourcesClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new Ressources());
		}
	}
}
