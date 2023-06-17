using FindYourPath.Views.Private.Agenda.SaveAgenda;
using Google.Apis.Calendar.v3.Data;
using Google.Protobuf.WellKnownTypes;
using GoogleApi.Entities.Search.Video.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.Forms.Device;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace FindYourPath.Views.Private.Agenda.SaveAgenda
{
	public class EventService
	{
		private string apiUrl;
		private static HttpClient _httpClient = new HttpClient();

		public EventService(string apiUrl)
		{
			this.apiUrl = apiUrl;
		}

		public async Task<ObservableCollection<MyScheduleAppointment>> GetEventsAsync(int userId)
		{
			var apiUrl = "http://dracken24.duckdns.org/apiAnnaLoup/actions" + "/EventService.php?action=read&userId=" + userId;
			var response = await _httpClient.GetAsync(apiUrl).ConfigureAwait(false);

			//TODO: a proteger avec try and catch 
			var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			var events = JsonConvert.DeserializeObject<ObservableCollection<MyScheduleAppointment>>(jsonResponse);

			foreach (var ev in events)
			{
				Console.WriteLine($"Event: {ev.Title}, StartDate: {ev.StartDate}, EndDate: {ev.EndDate}, Location: {ev.Location}, ..."); // ajoutez d'autres propriétés si nécessaire
			}

			return events;

		}


		public async Task SaveEventAsync(MyScheduleAppointment appointment)
		{
			var jsonObject = new JObject
			{
				["action"] = "createCalendarEvent",
				["Title"] = appointment.Title,
				["Description"] = appointment.Description,
				["StartDate"] = appointment.StartDate,
				["EndDate"] = appointment.EndDate,
				["StartTime"] = appointment.StartTime,
				["EndTime"] = appointment.EndTime,
				["Location"] = appointment.Location,
				["UserId"] = appointment.UserId
			};

			// TODO: remove writeline after debugging
			Console.WriteLine("******************************** PHP DEBUG ********************************");
			Console.WriteLine(jsonObject.ToString());
			var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
			var response = await Task.Run(() => _httpClient.PostAsync(App.ConnectionString + "/EventService.php", content));
			Console.WriteLine(response.StatusCode);
			Console.WriteLine("******************************** PHP DEBUG END ********************************");

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Server error response: {errorContent}");
			}

		}

		// Ajoutez des méthodes pour toutes les autres opérations.
	}
}
