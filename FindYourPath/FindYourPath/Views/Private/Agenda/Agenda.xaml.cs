using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using SQLite;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindYourPath.Views
{
	public partial class Agenda : ContentPage
	{
		// Créez une collection pour stocker vos événements.
		private ObservableCollection<MyScheduleAppointment> appointments;
		// Pour sauvegerder les infos sur la database
		private EventService eventService;

		public Agenda()
		{
			InitializeComponent();

			Title = "Agenda";

			eventService = new EventService(App.ConnectionString);
			appointments = new ObservableCollection<MyScheduleAppointment>();
			schedule.DataSource = appointments;
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await InitializeAsync();
		}

		public async Task InitializeAsync()
		{
			Console.WriteLine("************** HHHHEEEEELLLLLLPPPPP **************");
			try
			{
				await LoadEventsFromDatabase(App.User.GetUserId());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}

		private async Task LoadEventsFromDatabase(int userId)
		{
			appointments.Clear();

			var events = await eventService.GetEventsAsync(userId);
			foreach (var e in events)
			{
				appointments.Add(e);
			}
		}


		private void OnAddEventButtonClicked(object sender, EventArgs e)
		{

			// Récupérer la date sélectionnée
			var selectedDate = schedule.SelectedDate;
			if (!selectedDate.HasValue)
			{
				selectedDate = DateTime.Now;
			}

			User user = App.User;
			Navigation.PushAsync(new AddEventPage(appointments, selectedDate.Value, eventService, user));

		}

		private void OnDateClicked(object sender, CellTappedEventArgs e)
		{
			var selectedDate = e.Datetime;
			var eventsForDay = GetEventsForDay(selectedDate);
			eventList.ItemsSource = eventsForDay;
		}

		private void OnEventTapped(object sender, ItemTappedEventArgs e)
		{
			var selectedEvent = e.Item as MyScheduleAppointment;
			Navigation.PushAsync(new EventDetailPage(selectedEvent));
		}


		private List<MyScheduleAppointment> GetEventsForDay(DateTime date)
		{
			appointments = appointments ?? new ObservableCollection<MyScheduleAppointment>();

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
