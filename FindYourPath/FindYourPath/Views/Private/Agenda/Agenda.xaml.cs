using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.Core.Collections;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Agenda : ContentPage
	{
		EventCalendarViewModel _eventView;
		private EventService _eventService;

		public Agenda()
		{
			try
			{
				InitializeComponent();
				_eventService = new EventService(App.ConnectionString);
				_eventView = new EventCalendarViewModel(this, _eventService);
				BindingContext = _eventView;

			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (App.Events.Count == 0)
			{
				// ici vous pouvez attendre votre méthode Initialize
				await (BindingContext as EventCalendarViewModel).Initialize();
			}
			foreach (var day in _eventView.EventCalendar.Days)
			{
				day.Events.ReplaceRange(App.Events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}
		}

		private async void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			_eventView.OnAddEventButtonClicked(sender, e);
		}

		private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Console.WriteLine("OnCollectionViewSelectionChanged");
			var selectedEvent = e.CurrentSelection.FirstOrDefault() as MyEvent;
			if (selectedEvent != null)
			{
				Navigation.PushAsync(new EventDetailPage(selectedEvent, _eventService));

				((CollectionView)sender).SelectedItem = null;
				//_eventView.EventCalendar.SelectedDates.Clear();
			}
		}
	}
}

/*
using FindYourPath.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace FindYourPath.Views
{
	public partial class Agenda : ContentPage
	{
		// Create a collection to store your events.
		private ObservableCollection<Google.Apis.Calendar.v3.Data.Event> events;

		public Agenda()
		{
			InitializeComponent();

			Title = "Agenda";

			// Initialize the collection.
			events = new ObservableCollection<Google.Apis.Calendar.v3.Data.Event>();

			// Get the events from Google Calendar
			GetGoogleEvents();
		}

		private void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			// Open here your page or dialog to add an event
			Navigation.PushAsync(new AddEventPage(events));
		}

		private void OnEventTapped(object sender, ItemTappedEventArgs e)
		{
			var selectedEvent = e.Item as Google.Apis.Calendar.v3.Data.Event;
			Navigation.PushAsync(new EventDetailPage(selectedEvent));
		}

		private async void GetGoogleEvents()
		{
			// Implement the logic to retrieve the events from Google Calendar.
		}
	}
}
*/



/*
 * Veuillez noter que vous aurez besoin de credentials.json pour l'authentification
 * OAuth avec Google API. Vous pouvez obtenir ce fichier en créant un projet sur Google
 * Cloud Console et en activant l'API Google Calendar pour votre projet.

Aussi, dans le cadre de votre application Xamarin, vous devrez manipuler l'authentification
OAuth d'une manière qui fonctionne bien avec les appareils mobiles. Vous pouvez utiliser
des bibliothèques comme Xamarin.Auth pour aider à cela.

Enfin, ce code doit être exécuté de manière asynchrone. Cela signifie que vous devrez
probablement ajuster la façon dont votre application gère le chargement et la mise à
jour des événements pour s'assurer que l'interface utilisateur reste réactive pendant
que ces opérations sont en cours.

Le fichier credentials.json est très sensible et doit être sécurisé. Ne le partagez
jamais publiquement et ne le stockez pas dans votre code source.
 */
