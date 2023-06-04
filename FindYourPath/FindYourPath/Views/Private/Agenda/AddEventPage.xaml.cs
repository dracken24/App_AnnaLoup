using FindYourPath.Views.Private.Agenda.SaveAgenda;
using FreeSql.Internal.ObjectPool;
using GoogleApi.Entities.Maps.AerialView.Common;
using Syncfusion.SfSchedule.XForms;

using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
		ObservableCollection<MyScheduleAppointment> appointments;
		private EventService eventService;

		public AddEventPage(ObservableCollection<MyScheduleAppointment> appointments, DateTime selectedDate, EventService eventService)
		{
			InitializeComponent();
			this.appointments = appointments;
			this.eventService = eventService;
			startDatePicker.Date = selectedDate;
			endDatePicker.Date = selectedDate;
			startTimePicker.Time = new TimeSpan(8, 0, 0);
			endTimePicker.Time = new TimeSpan(10, 0, 0);
		}

		private void OnSaveButtonClicked(object sender, EventArgs e)
		{
			// Here you would save your event to your data source
			// and then navigate back to the calendar page

			// Combine date from startDatePicker and time from startTimePicker
			var startDate = startDatePicker.Date;
			var startTime = startTimePicker.Time;
			var startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hours, startTime.Minutes, startTime.Seconds);

			// Combine date from endDatePicker and time from endTimePicker
			var endDate = endDatePicker.Date;
			var endTime = endTimePicker.Time;
			var endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hours, endTime.Minutes, endTime.Seconds);

			var newEvent = new MyScheduleAppointment
			{
				Subject = titleEntry.Text,
				StartTime = startDateTime,
				EndTime = endDateTime,
				Notes = descriptionEditor.Text
			};

			// Add newEvent to your data source
			appointments.Add(newEvent);

			// Add newEvent to database
			CreateEvent(newEvent);

			// Then navigate back to the previous page
			Navigation.PopAsync();
		}

		public async Task CreateEvent(MyScheduleAppointment newEvent)
		{
			// Ajoutez ici toute autre logique nécessaire pour créer un événement...

			// Enregistrez l'événement dans la base de données
			await eventService.SaveEventAsync(newEvent);
		}
	}
}
