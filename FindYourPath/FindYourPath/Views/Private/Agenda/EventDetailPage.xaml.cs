using FindYourPath.Views.Private.Agenda.SaveAgenda;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FindYourPath.Views.Private.Agenda;
using System.Xml;

namespace FindYourPath.Views
{
	public partial class EventDetailPage : ContentPage
	{
		public EventDetailPage(Event appointment)
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
		}
	}
}
