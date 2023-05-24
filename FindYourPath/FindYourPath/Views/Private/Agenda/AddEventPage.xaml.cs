using FreeSql.Internal.ObjectPool;
using GoogleApi.Entities.Maps.AerialView.Common;
using Syncfusion.SfSchedule.XForms;

using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
		ObservableCollection<ScheduleAppointment> appointments;
		public AddEventPage(ObservableCollection<ScheduleAppointment> appointments, DateTime selectedDate)
		{
			InitializeComponent();
			this.appointments = appointments;
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
