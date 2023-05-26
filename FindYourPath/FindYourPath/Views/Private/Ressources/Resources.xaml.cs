using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FindYourPath.Views
{
	public partial class Resources : ContentPage
	{
		private ObservableCollection<CommunityResource> _resources;

		public Resources()
		{
			InitializeComponent();

			Title = "Resources";

			_resources = new ObservableCollection<CommunityResource>();
			resourceList.ItemsSource = _resources;
		}

		private void OnAddResourceButtonClicked(object sender, System.EventArgs e)
		{
			var newResource = new CommunityResource
			{
				Name = nameEntry.Text,
				Address = addressEntry.Text,
				PhoneNumber = phoneEntry.Text,
				Type = type.Text,
			};

			_resources.Add(newResource);

			// Clear the entries
			nameEntry.Text = string.Empty;
			addressEntry.Text = string.Empty;
			phoneEntry.Text = string.Empty;
			type.Text = string.Empty;
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var resource = (CommunityResource)e.Item;
			await Navigation.PushAsync(new ResourceDetailPage(resource));
		}

		/*
		namespace FindYourPath.Views
		{
			public partial class ResourceDetailPage : ContentPage
			{

				public ResourceDetailPage(CommunityResource resource)
				{
					Title = resource.Name;
			
					Content = new StackLayout
					{
						Children = {
							new Label { Text = "Nom          : " + resource.Name, TextColor = Color.FromHex("#006666") },
							new Label { Text = "Adresse    : " + resource.Address, TextColor = Color.FromHex("#006666") },
							new Label { Text = "Telephone: " + resource.PhoneNumber, TextColor = Color.FromHex("#006666") },
							new Label { Text = "Type          : " + resource.PhoneNumber, TextColor = Color.FromHex("#006666") },
						}
					};
			
				}
			}
		}
		*/

	}
}
