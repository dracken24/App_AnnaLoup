using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using FindYourPath.DataBase;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


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
		//var httpClient = new HttpClient();

		Console.Write("------------------------------BBBBBBBBBBBBB------------------------------\n");
		var content = new StringContent(JsonConvert.SerializeObject(paramJson), Encoding.UTF8, "application/json");

		var response = await _httpClient.PostAsync("http://dracken24.duckdns.org/apiAnnaLoup/actions/createAccount.php", content);

		if (response.IsSuccessStatusCode)
		{
			var responseContent = await response.Content.ReadAsStringAsync();

			// Déplace le traitement des données sur un autre thread
			var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent));

			if (!(bool)result["success"])
			{
				// Gérer l'erreur ici
				throw new Exception(result["error"].ToString());
			}
		}
		else
		{
			// Gérer l'erreur ici
			// Console.Write("------------------------------CANT CONNECT------------------------------");
			throw new Exception($"Une erreur est survenue lors de la création de votre compte: {response.StatusCode}");
		}
		Console.Write("------------------------------CCCCCCCCCCCC------------------------------\n");
	}

	public async Task<bool> VerifyUserAsync(JObject paramJson)
	{
		var content = new StringContent(JsonConvert.SerializeObject(paramJson), Encoding.UTF8, "application/json");

		var response = await _httpClient.PostAsync("http://dracken24.duckdns.org/apiAnnaLoup/actions/connectUser.php", content);
		Console.WriteLine("JSON REQUEST: " + response.Content.ReadAsStringAsync());

		if (response.IsSuccessStatusCode)
		{
			var jsonResponse = await response.Content.ReadAsStringAsync();

			// Déplace le traitement des données sur un autre thread
			var responseObject = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse));

			// Note the change here: we now check the "success" value in the JSON response
			if (responseObject.ContainsKey("success") && (bool)responseObject["success"])
			{
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
