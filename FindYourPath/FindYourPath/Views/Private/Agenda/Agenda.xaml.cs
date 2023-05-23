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
			
			// Ouvrez ici votre page ou votre fenêtre de dialogue pour ajouter un événement
			Navigation.PushAsync(new AddEventPage(appointments));
		}

		private void OnDateClicked(object sender, CellTappedEventArgs e)
		{
			var selectedDate = e.Datetime;
			var eventsForDay = GetEventsForDay(selectedDate);
			eventList.ItemsSource = eventsForDay;
		}

		/*
		private void OnDateClicked(object sender, EventArgs e)
		{
			var calendar = sender as SfCalendar;
			var selectedDate = schedule.SelectedDate;
			if (selectedDate.HasValue)
			{
				var eventsForDay = GetEventsForDay(selectedDate.Value);
				eventList.ItemsSource = eventsForDay;
			}
		}
		*/

		private List<ScheduleAppointment> GetEventsForDay(DateTime date)
		{
			var eventsForDay = appointments.Where(appointment =>
				appointment.StartTime.Date <= date.Date &&
				appointment.EndTime.Date >= date.Date).ToList();

			return eventsForDay;
		}
	}
}
