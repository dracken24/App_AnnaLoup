using FindYourPath.Views.Finance;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FindYourPath.Services
{
	public class FinanceService
	{
		private string _apiUrl;
		private static HttpClient _httpClient = new HttpClient();

		public FinanceService(string apiUrl)
		{
			_apiUrl = apiUrl;
		}

		// Enregistrer un événement dans la base de données
		public async Task SaveFinanceAsync(Transaction transaction)
		{
			var jsonObject = new JObject
			{
				["action"] = "addTransaction",
				["Description"] = transaction.Description,
				["Montant_Achat"] = transaction.Montant_Achat,
				["Date_Achat"] = transaction.Date_Achat,
				["Type"] = transaction.Type.ToString(),
				["UserId"] = App.User._id,
			};

			var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
			var response = await Task.Run(() => _httpClient.PostAsync(App.ConnectionString + "/FinanceServices.php", content));

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Server error response: {errorContent}");
			}
		}

		// Loader les contactes d'un utilisateur à partir de la base de données
		public async Task GetRessourcesAsync(int userId)
		{
			var connectionUrl = _apiUrl + "/FinanceServices.php?action=read&UserId=" + userId;
			var response = await _httpClient.GetAsync(connectionUrl).ConfigureAwait(false);
			Console.WriteLine("*** Status code: " + response.StatusCode);

			// List<Transaction> ressourcesPrivate = new List<Transaction>();
			try
			{
				var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				Console.WriteLine("*** Get jsonResponse: " + jsonResponse);

				App.Transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonResponse);

				if (App.Transactions == null)
				{
					Console.WriteLine("ressourcesPrivate is null");
					App.Transactions = new List<Transaction>();
				}
			}
			catch (JsonException ex)
			{
				Console.WriteLine("Error deserializing JSON: " + ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
		}


		/*
		// Mettre à jour un événement dans la base de données
		public async Task UpdateEventAsync(OneRessource appointment)
		{
			var jsonObject = new JObject
			{
				["action"] = "updateContact", // supposons que l'action soit nommée 'updateCalendarEvent'
				["id"] = appointment.id, // l'id est probablement nécessaire pour identifier quel événement mettre à jour
				["name"] = appointment.Nom,
				["Adress"] = appointment.Adresse,
				["Phone"] = appointment.Telephone,
				["Url"] = appointment.URL,
				["Type"] = appointment.Type,
				["Description"] = appointment.Description,
				["UserId"] = appointment.User_Id
			};

			Console.WriteLine("Objet Json: " + jsonObject.ToString());

			var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");
			var response = await Task.Run(() => _httpClient.PostAsync(App.ConnectionString + "/RessourceServices.php", content));

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Server error response: {errorContent}");
			}
		}

		// Supprimer un événement dans la base de données
		public async Task<bool> DeleteEventAsync(OneRessource currentEvent)
		{
			// Créer un message de requête DELETE
			var request = new HttpRequestMessage(HttpMethod.Delete, App.ConnectionString + "/RessourceServices.php");

			// Ajouter l'ID de l'événement au corps de la requête
			var requestJson = JsonConvert.SerializeObject(new { action = "delete", id = currentEvent.id });
			request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

			// Envoyer la requête
			var response = await _httpClient.SendAsync(request);

			return response.IsSuccessStatusCode;
		}*/
	}
}
