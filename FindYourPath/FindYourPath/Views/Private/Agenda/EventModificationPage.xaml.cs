using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using GoogleApi.Entities.Maps.AerialView.Common;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.Core.Collections;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;

namespace FindYourPath.Views.Private.Agenda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventModificationPage : ContentPage
	{
		private EventService _eventService;
		private MyEvent _currentEvent;
		static User _user;

		public EventModificationPage(MyEvent appointments, EventService eventService, User user)
		{
			InitializeComponent();
			_eventService = eventService;
			_currentEvent = appointments;

			titleEntry.Text = appointments.Title;
			startDatePicker.Date = appointments.StartDate;
			endDatePicker.Date = appointments.EndDate;
			startTimePicker.Time = appointments.StartTime;
			endTimePicker.Time = appointments.EndTime;
			descriptionEditor.Text = appointments.Description;
			_user = user;
		}

		private async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			// Here you would save your event to your data source
			// and then navigate back to the calendar page
			Console.WriteLine("Save Modify event");

			// Combine date from startDatePicker and time from startTimePicker
			var startDate = startDatePicker.Date;
			var startTime = startTimePicker.Time;
			var startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hours, startTime.Minutes, startTime.Seconds);

			// Combine date from endDatePicker and time from endTimePicker
			var endDate = endDatePicker.Date;
			var endTime = endTimePicker.Time;
			var endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hours, endTime.Minutes, endTime.Seconds);

			MyEvent existingEvent = App.Events.FirstOrDefault(x => x.id == _currentEvent.id);
			Console.WriteLine("_currentEvent.MyId1: " + _currentEvent.id + " existingEvent: " + _currentEvent.id);

			if (existingEvent != null)
			{
				Console.WriteLine("Modify event");
				existingEvent.Title = titleEntry.Text;
				existingEvent.StartDate = startDateTime.Date; // Assign the date part
				existingEvent.StartTime = startDateTime.TimeOfDay; // Assign the time part
				existingEvent.EndDate = endDateTime.Date; // Assign the date part
				existingEvent.EndTime = endDateTime.TimeOfDay; // Assign the time part
				existingEvent.Description = descriptionEditor.Text;
				existingEvent.UserId = _user._id;
				existingEvent.Location = "Saint-Nean";
				_currentEvent = existingEvent;

				// Here you need to update the existingEvent in your data source (like database)
				await _eventService.UpdateEventAsync(_currentEvent);
			}

			// Then navigate back to the previous page
			await Navigation.PopToRootAsync();
		}

		private async void OnDeleteButtonClicked(object sender, EventArgs e)
		{
			var confirmResult = await DisplayAlert("Confirmation", "Are you sure you want to delete this event?", "Yes", "No");

			if (confirmResult)
			{
				// Supprimez l'événement de la base de données
				var deleteResult = await _eventService.DeleteEventAsync(_currentEvent);

				if (deleteResult)
				{
					// Remove event from local list
					App.Events.Remove(_currentEvent);

					// Puis revenez à la page précédente
					await Navigation.PopToRootAsync();
				}
				else
				{
					await DisplayAlert("Error", "Failed to delete event.", "OK");
				}
			}
		}
	}
}
