using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace FindYourPath.Views
{
	public partial class Agenda : ContentPage
	{
		// Créez une collection pour stocker vos événements.
		private ObservableCollection<ScheduleAppointment> appointments;

		public Agenda()
		{
			InitializeComponent();

			Title = "Agenda";

			// Initialisez la collection.
			appointments = new ObservableCollection<ScheduleAppointment>();

			// Liez la collection d'événements à votre SfSchedule.
			schedule.DataSource = appointments;
		}

		private void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			// Récupérer la date sélectionnée
			var selectedDate = schedule.SelectedDate;
			if (!selectedDate.HasValue)
			{
				selectedDate = DateTime.Now;
			}

			// Ouvrez ici votre page ou votre fenêtre de dialogue pour ajouter un événement
			Navigation.PushAsync(new AddEventPage(appointments, selectedDate.Value));
		}

		private void OnDateClicked(object sender, CellTappedEventArgs e)
		{
			var selectedDate = e.Datetime;
			var eventsForDay = GetEventsForDay(selectedDate);
			eventList.ItemsSource = eventsForDay;
		}

		private void OnEventTapped(object sender, ItemTappedEventArgs e)
		{
			var selectedEvent = e.Item as ScheduleAppointment;
			Navigation.PushAsync(new EventDetailPage(selectedEvent));
		}


		private List<ScheduleAppointment> GetEventsForDay(DateTime date)
		{
			var eventsForDay = appointments.Where(appointment =>
				appointment.StartTime.Date <= date.Date &&
				appointment.EndTime.Date >= date.Date).ToList();

			return eventsForDay;
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
