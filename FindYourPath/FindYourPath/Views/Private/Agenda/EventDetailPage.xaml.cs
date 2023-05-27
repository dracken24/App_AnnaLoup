using FindYourPath.Views.Private.Agenda.SaveAgenda;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	public partial class EventDetailPage : ContentPage
	{
		public EventDetailPage(MyScheduleAppointment appointment)
		{
			InitializeComponent();
			Title = "Détail de l'événement";

			var subjectLabel = new Label { Text = $"Sujet                   : {appointment.Subject}" };
			var startTimeLabel = new Label { Text = $"Heure de début : {appointment.StartTime}" };
			var endTimeLabel = new Label { Text = $"Heure de fin       : {appointment.EndTime}" };
			var notesLabel = new Label { Text = $"Notes                  : {appointment.Notes}" };

			subLabel.Text = subjectLabel.Text;
			startTLabel.Text = startTimeLabel.Text;
			endTLabel.Text = endTimeLabel.Text;
			noLabel.Text = notesLabel.Text;
		}
	}
}
