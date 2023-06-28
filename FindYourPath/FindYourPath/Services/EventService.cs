using Google.Apis.Calendar.v3.Data;
using Google.Protobuf.WellKnownTypes;
using GoogleApi.Entities.Search.Video.Common;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Core.Collections;

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

		// Loader les événements d'un utilisateur à partir de la base de données
		public async Task GetEventsAsync(int userId)
		{
			var connectionUrl = apiUrl + "/EventService.php?action=read&userId=" + userId;
			var response = await _httpClient.GetAsync(connectionUrl).ConfigureAwait(false);

			//TODO: a proteger avec try and catch 
			var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			App.Events = JsonConvert.DeserializeObject<ObservableRangeCollection<MyEvent>>(jsonResponse);

			foreach (var ev in App.Events)
			{
				ev.StartDate = ev.StartDate.Date + ev.StartTime;
				ev.EndDate = ev.EndDate.Date + ev.EndTime;
				Console.WriteLine($"MyId: {ev.id} Event: {ev.Title}, StartDate: {ev.StartDate}, EndDate: {ev.EndDate}, Location: {ev.Location}, ..."); // ajoutez d'autres propriétés si nécessaire
			}
		}

		// Enregistrer un événement dans la base de données
		public async Task SaveEventAsync(MyEvent appointment)
		{
			//appointment.StartDate += appointment.StartDate.TimeOfDay;
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

		// Mettre à jour un événement dans la base de données
		public async Task UpdateEventAsync(MyEvent appointment)
		{
			var jsonObject = new JObject
			{
				["action"] = "updateCalendarEvent", // supposons que l'action soit nommée 'updateCalendarEvent'
				["id"] = appointment.id, // l'id est probablement nécessaire pour identifier quel événement mettre à jour
				["Title"] = appointment.Title,
				["Description"] = appointment.Description,
				["StartDate"] = appointment.StartDate,
				["EndDate"] = appointment.EndDate,
				["StartTime"] = appointment.StartTime,
				["EndTime"] = appointment.EndTime,
				["Location"] = appointment.Location,
				["UserId"] = appointment.UserId
			};

			var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
			var response = await Task.Run(() => _httpClient.PostAsync(App.ConnectionString + "/EventService.php", content));

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Server error response: {errorContent}");
			}
		}

		// Supprimer un événement dans la base de données
		public async Task<bool> DeleteEventAsync(MyEvent currentEvent)
		{
			// Créer un message de requête DELETE
			var request = new HttpRequestMessage(HttpMethod.Delete, App.ConnectionString + "/EventService.php");

			// Ajouter l'ID de l'événement au corps de la requête
			var requestJson = JsonConvert.SerializeObject(new { action = "delete", id = currentEvent.id });
			request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

			// Envoyer la requête
			var response = await _httpClient.SendAsync(request);

			return response.IsSuccessStatusCode;
		}
	}
}
