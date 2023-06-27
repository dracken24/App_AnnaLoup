using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FindYourPath.DataBase
{
	public class UserService
	{
		private readonly string _connectionString;
		private static HttpClient _httpClient = new HttpClient();

		public UserService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task AddUser(JObject paramJson)
		{
			// Envoie les request par .json au fichiers.php du server TODO: Changer lien selon le server
			var content = new StringContent(JsonConvert.SerializeObject(paramJson), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(_connectionString + "/createAccount.php", content);
			Console.WriteLine("Responce content: " + response.Content);

			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();

				// Déplace le traitement des données sur un autre thread
				var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent));
				Console.WriteLine("Result: " + result);
				Console.WriteLine("success: " + result["success"]);
				Console.WriteLine("User: " + result["user"]);

				/*
				if (!(bool)result["success"])
				{
					Console.WriteLine("ERROR: " + Console.Error);
					// Gérer l'erreur ici
					throw new Exception(result["error"].ToString());
				}
				*/

				// Note the change here: we now check the "success" value in the JSON response
				if (result.ContainsKey("success") && (bool)result["success"])
				{
					//App app = new App();
					App.SaveUser(result["user"]);
					Console.WriteLine("*** USER ***: " + result["user"]); // infos du user
				}
				else
				{
					// Optionally, log the error message if present
					if (result.ContainsKey("error"))
					{
						Console.WriteLine("Error from server: " + result["error"]);
					}
				}
				//App.SaveUser(result["user"]);
			}
			else
			{
				// Gérer l'erreur ici
				throw new Exception($"Une erreur est survenue lors de la création de votre compte: {response.StatusCode}");
			}
		}

		public async Task<bool> ConnectUser(JObject paramJson)
		{
			Console.WriteLine("***************************** in connect ********************************");
			// Envoie les request par .json au fichiers.php du server TODO: Changer lien selon le server
			var content = new StringContent(JsonConvert.SerializeObject(paramJson), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(_connectionString + "/connectUser.php", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Déplace le traitement des données sur un autre thread
				var responseObject = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse));
				Console.WriteLine("Result: " + responseObject);
				Console.WriteLine("success: " + responseObject["success"]);
				Console.WriteLine("User: " + responseObject["user"]);

				// Note the change here: we now check the "success" value in the JSON response
				if (responseObject.ContainsKey("success") && (bool)responseObject["success"])
				{
					//App app = new App();
					App.SaveUser(responseObject["user"]);
					Console.WriteLine("*** USER ***: " + responseObject["user"]); // infos du user
					return true;
				}
				else
				{
					// Optionally, log the error message if present
					if (responseObject.ContainsKey("error"))
					{
						Console.WriteLine("Error from server: " + responseObject["error"]);
					}
					return false;
				}
			}
			else
			{
				// Handle unsuccessful HTTP response here...
				Console.WriteLine("Unsuccessful HTTP response. Status code: " + response.StatusCode);
				return false;
			}
		}
	}
}
