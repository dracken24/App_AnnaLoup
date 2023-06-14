using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views.Public.RessourcesPublic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OneRessourceListPage : ContentPage
	{
		ListView listView;
		public OneRessourceListPage(Dictionary<string, OneRessource> options)
		{
			this.Title = "Liste de Ressources";
			listView = new ListView();
			listView.ItemsSource = options.Values;
			listView.ItemTapped += OnItemTapped;
			Content = new StackLayout
			{
				Children = { listView }
			};
		}

		async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item != null)
			{
				var resource = (OneRessource)e.Item;
				await Navigation.PushAsync(new OneRessourceDetailPage(resource));
			}
		}
	}
}