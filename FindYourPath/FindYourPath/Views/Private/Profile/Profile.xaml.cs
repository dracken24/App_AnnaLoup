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
		private ProfileViewModel _viewModel;

		public Profile()
		{
			InitializeComponent();
			ProfileViewModel viewModel = new ProfileViewModel { Name = App.User._name, Address = App.User._adress, Email = App.User._email, Phone = App.User._phone };

			_viewModel = viewModel;
			this.BindingContext = _viewModel;
		}

		public Profile(ProfileViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			this.BindingContext = _viewModel;
		}

		public async void UpdateProfileInfos(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ChangeProfilInfos(_viewModel));
		}
	}
}