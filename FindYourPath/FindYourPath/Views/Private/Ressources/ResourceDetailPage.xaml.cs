using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindYourPath.Views
{
	public partial class ResourceDetailPage : ContentPage
	{

		public ResourceDetailPage(CommunityResource resource)
		{
			InitializeComponent();

			Title = resource.Name;

			nameLabel.Text = $"Nom           : {resource.Name}";
			addressLabel.Text = $"Adresse     : {resource.Address}";
			phoneLabel.Text = $"Téléphone : {resource.PhoneNumber}";
			typeLabel.Text = $"Type           : {resource.Type}";
		}
	}
}