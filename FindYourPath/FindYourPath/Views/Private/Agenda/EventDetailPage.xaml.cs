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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
		
		public EventDetailPage(ScheduleAppointment appointment)
		{
			Title = "Détail de l'événement";

			var subjectLabel = new Label { Text = $"Sujet: {appointment.Subject}" };
			var startTimeLabel = new Label { Text = $"Heure de début: {appointment.StartTime}" };
			var endTimeLabel = new Label { Text = $"Heure de fin: {appointment.EndTime}" };
			var notesLabel = new Label { Text = $"Notes: {appointment.Notes}" };

			subLabel.Text = subjectLabel.Text;
			startTLabel.Text = startTimeLabel.Text;
			endTLabel.Text = endTimeLabel.Text;
			noLabel.Text = notesLabel.Text;
		}
		/*

		public EventDetailPage(CommunityResource resource)
		{
			Title = resource.Name;

			nameLabel.Text = $"Nom: {resource.Name}";
			addressLabel.Text = $"Adresse: {resource.Address}";
			phoneLabel.Text = $"Téléphone: {resource.PhoneNumber}";
			typeLabel.Text = $"Type: {resource.Type}";
		}*/
	}
}
