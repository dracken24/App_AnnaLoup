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
	public partial class OneRessourceDetailPage : ContentPage
	{
		public OneRessourceDetailPage(OneRessource resource)
		{
			InitializeComponent();
			

			Title = resource.Name;

			nameLabel.Text = $"Nom            :";
			addressLabel.Text = $"Adresse      :";
			phoneLabel.Text = $"Téléphone  :";
			urlLabel.Text = $"Url                :";
			descriptionLabel.Text = $"Description :";

			nameDescription.Text = resource.Name;
			addressDescription.Text = resource.Adress;
			phoneDescription.Text = resource.Phone;
			urlDescription.Text = resource.Url;
			descriptionDescription.Text = resource.Description;
		}
	}
}