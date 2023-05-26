using FindYourPath.Views.Private.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Profile : ContentPage
	{
		private ProfileViewModel viewModel;

		public Profile()
		{
			InitializeComponent();
			ProfileViewModel viewModel = new ProfileViewModel { Name = "Scrat Oak", Address = "1424 Somwere in a big lost tree", Email = "ILoveNuts@iceage.cold", Phone = "1-888-ILoveNuts" };

			this.viewModel = viewModel;

			nameLabel.SetBinding(Label.TextProperty, new Binding("Name", source: viewModel));
			addressLabel.SetBinding(Label.TextProperty, new Binding("Address", source: viewModel));
			emailLabel.SetBinding(Label.TextProperty, new Binding("Email", source: viewModel));
			phoneLabel.SetBinding(Label.TextProperty, new Binding("Phone", source: viewModel));
		}

		public Profile(ProfileViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;

			nameLabel.SetBinding(Label.TextProperty, new Binding("Name", source: viewModel));
			addressLabel.SetBinding(Label.TextProperty, new Binding("Address", source: viewModel));
			emailLabel.SetBinding(Label.TextProperty, new Binding("Email", source: viewModel));
			phoneLabel.SetBinding(Label.TextProperty, new Binding("Phone", source: viewModel));
		}

		public async void UpdateProfileInfos(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ChangeProfilInfos(viewModel));
			Console.WriteLine("NAME: " + viewModel.Name);
		}
	}
}