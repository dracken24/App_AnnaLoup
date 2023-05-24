using FreeSql.Internal.ObjectPool;
using GoogleApi.Entities.Maps.AerialView.Common;
using Syncfusion.SfSchedule.XForms;

using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
		ObservableCollection<ScheduleAppointment> appointments;
		public AddEventPage(ObservableCollection<ScheduleAppointment> appointments)
		{
			InitializeComponent();
			this.appointments = appointments;
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

			var newEvent = new ScheduleAppointment
			{
				Subject = titleEntry.Text,
				StartTime = startDateTime,
				EndTime = endDateTime,
				Notes = descriptionEditor.Text
			};

			// Add newEvent to your data source
			appointments.Add(newEvent);

			// Then navigate back to the previous page
			Navigation.PopAsync();
		}
	}
}

/*
using Google.Apis.Calendar.v3.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FindYourPath.Services;
using Syncfusion.SfSchedule.XForms;
using System.Collections.ObjectModel;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
		ObservableCollection<Google.Apis.Calendar.v3.Data.Event> events;
		private AgendaService agendaService;

		public AddEventPage(ObservableCollection<Google.Apis.Calendar.v3.Data.Event> events)
		{
			InitializeComponent();
			this.events = events;
			this.agendaService = new AgendaService("your_api_key"); // replace with your Google API Key
		}

		private async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			var startDateTime = startDatePicker.Date + startTimePicker.Time;
			var endDateTime = endDatePicker.Date + endTimePicker.Time;

			var newEvent = agendaService.CreateEvent(startDateTime, endDateTime, titleEntry.Text, descriptionEditor.Text);
			await agendaService.AddEvent("primary", newEvent);

			Navigation.PopAsync();
		}
	}
}
*/
