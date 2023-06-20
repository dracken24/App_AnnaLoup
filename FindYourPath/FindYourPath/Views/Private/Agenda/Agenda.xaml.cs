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
		EventCalendarExampleViewModel _eventView;

		public Agenda()
		{
			try
			{
				InitializeComponent();
				_eventView = new EventCalendarExampleViewModel(this);
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

			// ici vous pouvez attendre votre méthode Initialize
			ObservableRangeCollection<Event> events = await (BindingContext as EventCalendarExampleViewModel).Initialize();
			foreach (var day in _eventView.EventCalendar.Days)
			{
				day.Events.ReplaceRange(events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}
		}

		private async void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			_eventView.OnAddEventButtonClicked(sender, e);
		}

		private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Console.WriteLine("OnCollectionViewSelectionChanged");
			var selectedEvent = e.CurrentSelection.FirstOrDefault() as Event;
			if (selectedEvent != null)
			{
				Console.WriteLine("selectedEvent != null");
				// Do something with the selected item
				// For example, navigate to a new page with the event details
				Navigation.PushAsync(new EventDetailPage(selectedEvent));
			}
		}
	}
}

/*
using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using SQLite;

//using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XCalendar.Core.Models;
using XCalendar;
using Google.Apis.Calendar.v3.Data;
using FindYourPath.Views.Private.Agenda;
using FindYourPath.Converters.FindYourPath.Converters;

namespace FindYourPath.Views
{
	public partial class Agenda : ContentPage
	{
		// Créez une collection pour stocker vos événements.
		private ObservableCollection<MyScheduleAppointment> appointments;
		// Pour sauvegerder les infos sur la database
		private EventService eventService;
		private CalendarDayToEventsConverter _converter;
		public ObservableCollection<DayModel> days { get; set; }


		public Agenda()
		{
			InitializeComponent();
			this.BindingContext = this;

			Title = "Agenda";

			_converter = new CalendarDayToEventsConverter(GetEventsForDay);

			Resources.Add("CalendarDayToEventsConverter", _converter);

			eventService = new EventService(App.ConnectionString);
			appointments = new ObservableCollection<MyScheduleAppointment>();

			days = new ObservableCollection<DayModel>();
		}

		// Remplie le calendrier avec les événements de la database
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await InitializeAsync();
		}

		// Retourne une liste d'événements pour une date donnée.
		private void CalendarView_DateSelectionChanged(object sender, EventArgs e)
		{
			if (calendar.SelectedDates.Count == 0) // Si une date n'est pas sélectionnée ou deselectionnée
			{
				eventList.ClearValue(ListView.ItemsSourceProperty);
				return;
			}
			else if (calendar.SelectedDates.Count > 1) // Si plusieurs dates sont sélectionnées
			{
				calendar.SelectedDates.RemoveAt(0);
			}
			
			eventList.ItemsSource = GetEventsForDay(calendar.SelectedDates[0]); // Affiche les événements de la date sélectionnée
		}

		public async Task InitializeAsync()
		{
			try
			{
				await Task.Run(() => LoadEventsFromDatabase(App.User.GetUserId()));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}

		private async Task LoadEventsFromDatabase(int userId)
		{
			appointments.Clear();

			if (eventService == null)
			{
				Console.WriteLine("eventService is null");
				return;
			}

			var events = await eventService.GetEventsAsync(userId);
			if (events == null)
			{
				Console.WriteLine("GetEventsAsync returned null");
				return;
			}

			foreach (var e in events)
			{
				appointments.Add(e);
			}
		}

		private void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			// Récupérer la date sélectionnée
			var selectedDate = calendar.SelectedDates[0];
			if (selectedDate == null)
			{
				selectedDate = DateTime.Now;
			}

			User user = App.User;
			Navigation.PushAsync(new AddEventPage(appointments, selectedDate, eventService,calendar , user));
		}

		// Afficher les détails de l'événement sélectionné.
		private void OnEventTapped(object sender, ItemTappedEventArgs e)
		{
			var selectedEvent = e.Item as MyScheduleAppointment;
			Navigation.PushAsync(new EventDetailPage(selectedEvent));
		}

		// Retourn une liste d'événements pour une date donnée.
		private List<MyScheduleAppointment> GetEventsForDay(DateTime date)
		{
			var localAppointments = appointments;

			var eventsForDay = localAppointments.Where(appointment =>
				appointment.StartDate.Date <= date.Date &&
				appointment.EndDate.Date >= date.Date).ToList();

			foreach (var eventForDay in eventsForDay)
			{
				// Nous ajoutons simplement un nouvel objet DayModel pour chaque événement
				days.Add(new DayModel() { Events = new List<MyScheduleAppointment> { eventForDay } });

				Console.WriteLine(eventForDay.Title);
			}

			return eventsForDay;
		}

	}
}
*/

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
