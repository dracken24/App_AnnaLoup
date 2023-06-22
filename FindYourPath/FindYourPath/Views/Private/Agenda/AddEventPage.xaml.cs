using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using GoogleApi.Entities.Maps.AerialView.Common;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
		private EventService eventService;
		static User _user;

		public AddEventPage(DateTime selectedDate,
			EventService eventService, User user)
		{
			InitializeComponent();
			this.eventService = eventService;

			startDatePicker.Date = selectedDate;
			endDatePicker.Date = selectedDate;
			startTimePicker.Time = new TimeSpan(8, 0, 0);
			endTimePicker.Time = new TimeSpan(10, 0, 0);
			_user = user;
		}

		private async void OnSaveButtonClicked(object sender, EventArgs e)
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

			Event newEvent = new Event
			{
				Title = titleEntry.Text,
				StartDate = startDateTime.Date, // Assign the date part
				StartTime = startDateTime.TimeOfDay, // Assign the time part
				EndDate = endDateTime.Date, // Assign the date part
				EndTime = endDateTime.TimeOfDay, // Assign the time part
				Description = descriptionEditor.Text,
				UserId = _user.GetUserId(),
				Location = "Saint-Nean"
			};

			Console.WriteLine("NewEvent: " + newEvent.PrintMembers());

			try
			{
				// Add newEvent to your data source
				App.Events.Add(newEvent); // Add to the local ObservableCollection
				// calendar.SelectedDates.Add(newEvent.StartDate); // Add to the calendar's SelectedDates
				

				// Add newEvent to database
				await Task.Run(() => CreateEvent(newEvent));
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine("HTTP, Une erreur s'est produite lors de la connexion au serveur : " + ex);
				await DisplayAlert("Erreur", "Impossible de se connecter au serveur.", "OK");
			}
			catch (JsonException ex)
			{
				Console.WriteLine("JSON, Une erreur s'est produite lors de l'analyse de la réponse du serveur : " + ex);
				await DisplayAlert("Erreur", "Une erreur inattendue s'est produite. " + ex.Message, "OK");
			}
			catch (Exception ex)
			{
				Console.WriteLine("STANDARD, " + ex.ToString());
			}

			// Then navigate back to the previous page
			await Navigation.PopAsync();
		}

		public async Task CreateEvent(Event newEvent)
		{
			// Ajoutez ici toute autre logique nécessaire pour créer un événement...
			Console.WriteLine("*************************** EVENT **************************");

			// Enregistrez l'événement dans la base de données
			await eventService.SaveEventAsync(newEvent);
			App.Events.ReplaceRange(App.Events.Where(x => x.StartDate.Date == newEvent.StartDate));
		}
	}
}
