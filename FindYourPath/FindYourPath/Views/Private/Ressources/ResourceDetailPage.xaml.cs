using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FindYourPath.Views
{
	public partial class ResourceDetailPage : ContentPage
	{
		Geocoder _geocoder;

		public ResourceDetailPage(CommunityResource resource)
		{
			InitializeComponent();

			Title = resource.Name;

			nameLabel.Text = $"Nom           : {resource.Name}";
			addressLabel.Text = $"Adresse     : {resource.Address}";
			phoneLabel.Text = $"Téléphone : {resource.PhoneNumber}";
			typeLabel.Text = $"Type           : {resource.Type}";
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