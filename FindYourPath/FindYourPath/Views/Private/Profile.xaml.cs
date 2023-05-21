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
		public Profile()
		{
			InitializeComponent();

			Title = "Profile";

			// Mettez à jour ces valeurs avec les informations de l'utilisateur.
			nameLabel.Text = "Scrat Oak";
			addressLabel.Text = "Somwere in a tree";
			phoneLabel.Text = "1 888 ILoveNuts";

			// Et la photo de profil. 
			// Vous pouvez utiliser une URL ou un chemin de fichier local ici.
			profileImage.Source = "Scrat_02.jpg";
		}
	}
}