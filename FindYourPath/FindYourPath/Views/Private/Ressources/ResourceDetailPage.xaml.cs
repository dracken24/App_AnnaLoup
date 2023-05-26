using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

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