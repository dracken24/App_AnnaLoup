using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Welcome : ContentPage
	{
		public Welcome()
		{
			Title = "Accueil";
			Console.WriteLine("***************************** WELCOME ******************************");
			App.PrintMembers();
			InitializeComponent();
		}
		private void OnSearchButtonClicked(object sender, EventArgs e)
		{
			SearchGoogle();
		}

		private async Task SearchGoogle()
		{
			var searchTerm = searchBar.Text;

			// Replace spaces with '+' symbol for URL
			var urlEncodedSearchTerm = searchTerm.Replace(' ', '+');

			// Open web browser with Google search for the term
			await Browser.OpenAsync($"https://www.google.ca/search?q={urlEncodedSearchTerm}", BrowserLaunchMode.SystemPreferred);
		}
	}
}
