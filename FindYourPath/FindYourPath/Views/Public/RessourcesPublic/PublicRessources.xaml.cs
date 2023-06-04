using FindYourPath.Properties;
using FindYourPath.Views.Private.Profile;
using FindYourPath.Views.Public.RessourcesPublic;
using Syncfusion.SfCalendar.XForms;
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
	public partial class PublicRessources : ContentPage
	{
		public PublicRessources()
		{
			InitializeComponent();

			Title = "Ressources Publique";
		}

		private Dictionary<string, List<ListeRessources>> cityResources = new Dictionary<string, List<ListeRessources>>
		{
			{
				"Quebec", new List<ListeRessources>
				{

					new ListeRessources
					{
						City = "Quebec",
						Name = "Aide Allimentaire",
						Options = new List<string> { "Quebec Option 1", "Option 2", "Option 3", "Option 3.5" }
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Aide Financiere",
						Options = new List<string> { "Quebec Option 4", "Option 5", "Option 6" }
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Aide Psycho Sociale",
						Options = new List<string> { "Quebec Option 7", "Option 8", "Option 9", "Option 9.5" }
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Hebergement",
						Options = new List<string> { "Quebec Option 10", "Option 11", "Option 12" }
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Emploies/Etudes",
						Options = new List<string> { "Quebec Option 13", "Option 14", "Option 15" }
					},
				}
			},
			{
				"Montreal", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Montreal",
						Name = "Aide Allimentaire",
						Options = new List<string> { "Montreal Option 11", "Option 12", "Option 13", "Option 13.5" }
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Aide Financiere",
						Options = new List<string> { "Montreal Option 14", "Option 15", "Option 16" }
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Aide Psycho Sociale",
						Options = new List<string> { "Montreal Option1 7", "Option 18", "Option 19", "Option 19.5" }
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Hebergement",
						Options = new List<string> { "Montreal Option 110", "Option 111", "Option 112" }
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Emploies/Etudes",
						Options = new List<string> { "Montreal Option 113", "Option 114", "Option 115" }
					},
				}
			},
			{
				"Outaouais", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Aide Allimentaire",
						Options = new List<string> { "Outaouais Option 21", "Option 22", "Option 23", "Option 23.5" }
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Aide Financiere",
						Options = new List<string> { "Outaouais Option 24", "Option 25", "Option 26" }
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Aide Psycho Sociale",
						Options = new List<string> { "Outaouais Option 27", "Option 28", "Option 29", "Option 29.5" }
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Hebergement",
						Options = new List<string> { "Outaouais Option 210", "Option 211", "Option 212" }
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Emploies/Etudes",
						Options = new List<string> { "Outaouais Option 213", "Option 14", "Option 215" }
					},
				}
			},
			{
				"Other", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Other",
						Name = "Aide Allimentaire",
						Options = new List<string> { "Other Option 31", "Option 32", "Option 33", "Option 33.5" }
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Aide Financiere",
						Options = new List<string> { "Other Option 4", "Option 35", "Option 36" }
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Aide Psycho Sociale",
						Options = new List<string> { "Other Option 37", "Option 38", "Option 39", "Option 39.5" }
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Hebergement",
						Options = new List<string> { "Other Option 310", "Option 311", "Option 312" }
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Emploies/Etudes",
						Options = new List<string> { "Other Option 313", "Option 314", "Option 315" }
					},
				}
			},

		};

		public void OnQuebecClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Quebec");
		}

		public void OnMontrealClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Montreal");
		}

		public void OnOutaouaisClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Outaouais");
		}

		public void OnOtherClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Other");
		}

		public void OnResourceSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				var resource = (ListeRessources)e.SelectedItem;

				// Mettre à jour le Label dans le bloc du bas de la page avec les informations de la ressource sélectionnée
				bottomListView.ItemsSource = resource.Options;
				//resourcesListView.SelectedItem = null;
			}
		}

		public void UpdateResourcesListView(string city)
		{
			if (cityResources.ContainsKey(city))
			{
				var resources = cityResources[city];

				// Mettez à jour la ListView
				resourcesListView.ItemsSource = resources;
			}
		}
	}
}
