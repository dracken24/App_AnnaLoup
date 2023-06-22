using System;

using Xamarin.Forms;
using XCalendar.Core.Collections;
using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using FindYourPath.Views.Private.Agenda;

//using Google.Apis.Calendar.v3.Data;

namespace FindYourPath.Views
{
	public partial class EventDetailPage : ContentPage
	{
		Event _currentEvent;
		EventService _eventService;

		public EventDetailPage(Event appointment, EventService eventService)
		{
			InitializeComponent();
			Title = "Détail de l'événement";

			var subjectLabelIn = new Label { Text = $"Sujet                    : "};
			var startTimeLabelIn = new Label { Text = $"Heure de début : " };
			var endTimeLabelIn = new Label { Text = $"Heure de fin       : " };
			var notesLabelIn = new Label { Text = $"Notes                   : " };

			subjectLabel.Text = subjectLabelIn.Text;
			startLabel.Text = startTimeLabelIn.Text;
			endLabel.Text = endTimeLabelIn.Text;
			descriptionLabel.Text = notesLabelIn.Text;

			subjectDescription.Text = appointment.Title;
			startDescription.Text = appointment.StartTime.ToString();
			endDescription.Text = appointment.EndTime.ToString();
			descriptionDescription.Text = appointment.Description;

			_currentEvent = appointment;
			_eventService = eventService;
		}

		private async void OnModifyButtonClicked(object sender, EventArgs e)
		{
			Console.WriteLine("Modify event");

			User user = App.User;
			Navigation.PushAsync(new EventModificationPage(_currentEvent, _eventService, user));

			// Then navigate back to the previous page
			//Navigation.PopAsync();
		}
	}
}
