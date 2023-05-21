using System;
using System.Collections.Generic;
using XamForms.Controls;
using Xamarin.Forms;
using System.Globalization;
using FindYourPath.Views;

namespace FindYourPath.Views
{
	public partial class Agenda : ContentPage
	{
		public Agenda()
		{
			InitializeComponent();

			Title = "Agenda";

			calendar.SelectedDate = DateTime.Today;
			calendar.SpecialDates = new List<SpecialDate>
			{
				new SpecialDate(DateTime.Today.AddDays(11)) { BackgroundColor = Color.MediumPurple, Selectable = true },
				new SpecialDate(DateTime.Today.AddDays(-9)) { BackgroundColor = Color.Green, Selectable = true }
			};
			calendar.DateClicked += (s, e) => {
				// logique pour afficher les événements de la journée ici
				// Par exemple :
				// var eventsForDay = GetEventsForDay(e.SelectedDate);
				// eventList.ItemsSource = eventsForDay;
			};
		}

		private void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			// Ouvrez ici votre page ou votre fenêtre de dialogue pour ajouter un événement
			// Par exemple :
			// Navigation.PushAsync(new AddEventPage());
		}
	}
}