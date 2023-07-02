using FindYourPath.Views;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FindYourPath.Services
{
	public class RessourceService
	{
		private string _apiUrl;
		private static HttpClient _httpClient = new HttpClient();

		public RessourceService(string apiUrl)
		{
			_apiUrl = apiUrl;
		}

		// Enregistrer un événement dans la base de données
		public async Task SaveRessourceAsync(OneRessource ressource)
		{

			var jsonObject = new JObject
			{
				["action"] = "addPrivateRessource",
				["UserId"] = App.User._id,
				["Nom"] = ressource.Nom,
				["Adresse"] = ressource.Adresse,
				["Phone"] = ressource.Telephone,
				["URL"] = ressource.URL,
				["Type"] = "",
				["Description"] = ressource.Description
			};

			Console.WriteLine(jsonObject.ToString());
			var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
			var response = await Task.Run(() => _httpClient.PostAsync(App.ConnectionString + "/RessourceServices.php", content));
			Console.WriteLine("Responce: " + response.StatusCode);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Server error response: {errorContent}");
			}
		}

		// Loader les contactes d'un utilisateur à partir de la base de données
		public async Task<ObservableCollection<OneRessource>> GetRessourcesAsync(int userId)
		{
			var connectionUrl = _apiUrl + "/RessourceServices.php?action=read&UserId=" + userId;
			var response = await _httpClient.GetAsync(connectionUrl).ConfigureAwait(false);
			Console.WriteLine("*** GetRessourcesAsync: " + response.StatusCode);

			//TODO: a proteger avec try and catch 
			var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			Console.WriteLine("*** Get jsonResponce: " + jsonResponse);
			Console.WriteLine("url connexion: " + connectionUrl);

			ObservableCollection<OneRessource> ressourcesPrivate = new ObservableCollection<OneRessource>();
			// ressourcesPrivate = JsonConvert.DeserializeObject<ObservableCollection<OneRessource>>(jsonResponse);
			try
			{
				ressourcesPrivate = JsonConvert.DeserializeObject<ObservableCollection<OneRessource>>(jsonResponse);
			}
			catch (JsonException ex)
			{
				Console.WriteLine("Error deserializing JSON: " + ex.Message);
			}

			if (ressourcesPrivate != null)
			{
				foreach (OneRessource ressource in ressourcesPrivate)
				{
					Console.WriteLine("Ressource: " + ressource.Nom);
				}
			}
			else
			{
				Console.WriteLine("ressourcesPrivate is null");
			}

			return ressourcesPrivate;
		}

		/*
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
		*/
	}
}
