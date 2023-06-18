using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResourceDetailPage : ContentPage
	{
		// Geocoder _geocoder;

		public ResourceDetailPage(CommunityResource resource)
		{
			InitializeComponent();

			Title = resource.Name;

			nameLabel.Text = $"Nom           : ";
			addressLabel.Text = $"Adresse     : ";
			phoneLabel.Text = $"Téléphone : ";
			urlLabel.Text = $"Url               : ";
			typeLabel.Text = $"Type           : ";

			nameDescription.Text = resource.Name;
			addressDescription.Text = resource.Address;
			phoneDescription.Text = resource.PhoneNumber;
			urlDescription.Text = "https://" + resource.Url;
			typeDescription.Text = resource.Type;
			/*
				_geocoder = new Geocoder();
				try
				{
					ShowLocationOnMap(resource.Address);
				}
				catch (Exception e)
				{
					// Print error if deserialization fails
					Console.WriteLine($"Failed to connect to google map error: {e.Message}");
				}
			*/
		}
		async void OnUrlTapped(object sender, EventArgs e)
		{
			try
			{
				var url = urlDescription.Text;
				if (!string.IsNullOrWhiteSpace(url))
				{
					await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to open browser error: {ex.Message}");
			}
		}

		/*
		private async void ShowLocationOnMap(string address)
		{
			var positions = await _geocoder.GetPositionsForAddressAsync(address);
			foreach (var position in positions)
			{
				MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.3)));
				var pin = new Pin
				{
					Type = PinType.Place,
					Position = position,
					Label = address,
					Address = address
				};
				MyMap.Pins.Add(pin);
			}
		}
		*/
	}
}