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

		public async Task<List<MyScheduleAppointment>> GetEventsAsync()
		{
			var response = await _httpClient.GetAsync(apiUrl + "/event.php?action=read");

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialisez la réponse en une liste de MyScheduleAppointment.
				var events = JsonConvert.DeserializeObject<List<MyScheduleAppointment>>(jsonResponse);

				return events;
			}
			else
			{
				// Handle unsuccessful HTTP response here...
				throw new Exception($"Failed to retrieve events. HTTP status: {response.StatusCode}");
			}
		}

		public async Task SaveEventAsync(MyScheduleAppointment appointment)
		{
			var jsonObject = new JObject
			{
				["action"] = "create",
				["Title"] = appointment.Subject,
				["Description"] = appointment.Notes,
				["StartDate"] = appointment.StartTime.Date,
				["EndDate"] = appointment.EndTime.Date,
				["StartTime"] = (int)appointment.StartTime.TimeOfDay.TotalSeconds,
				["EndTime"] = (int)appointment.EndTime.TimeOfDay.TotalSeconds,
				["Location"] = appointment.Location,
				["UserId"] = appointment.MyId
			};

			// TODO: remove writeline after debugging
			Console.WriteLine("******************************** PHP DEBUG ********************************");
			Console.WriteLine(jsonObject.ToString());
			var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("http://dracken24.duckdns.org/apiAnnaLoup/actions/EventService.php", content);
			Console.WriteLine(response.StatusCode);
			Console.WriteLine("******************************** PHP DEBUG END ********************************");

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Une erreur est survenue lors de la sauvegarde de l'événement: {response.StatusCode}");
			}
		}

		// Ajoutez des méthodes pour toutes les autres opérations.
	}

	/*
	public class EventService
	{

		readonly SQLiteAsyncConnection _database;

		public EventService(string dbPath)
		{
			_database = new SQLiteAsyncConnection(dbPath);
			//_database.CreateTableAsync<MyScheduleAppointment>().Wait();
		}

		public async Task InitializeAsync()
		{
			await _database.CreateTableAsync<Event>();
		}

		public Task<List<MyScheduleAppointment>> GetEventsAsync()
		{
			return _database.Table<MyScheduleAppointment>().ToListAsync();
		}

		public Task<int> SaveEventAsync(MyScheduleAppointment appointment)
		{
			if (appointment.MyId != 0)
			{
				return _database.UpdateAsync(appointment);
			}
			else
			{
				return _database.InsertAsync(appointment);
			}
		}
	}
	*/
}
