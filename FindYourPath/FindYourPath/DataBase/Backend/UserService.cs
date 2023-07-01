using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Mail;
using System.Net;

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
			// Console.WriteLine("Responce content: " + response.Content);

			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();

				// Déplace le traitement des données sur un autre thread
				var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent));

				// Console.WriteLine("User: " + result["user"]);

				// Note the change here: we now check the "success" value in the JSON response
				if (result.ContainsKey("success") && (bool)result["success"])
				{
					//App app = new App();
					App.SaveUser(result["user"]);
					SendConfirmationEmail(App.User._email, (App.User._verifEmail));
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
			}
			else
			{
				// Gérer l'erreur ici
				throw new Exception($"Une erreur est survenue lors de la création de votre compte: {response.StatusCode}");
			}
		}

		public async Task<bool> UpdateUser(JObject paramJson)
		{
			// Envoie les request par .json au fichiers.php du server TODO: Changer lien selon le server
			var content = new StringContent(JsonConvert.SerializeObject(paramJson), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(_connectionString + "/verifyAccount.php", content);
			// Console.WriteLine("Responce content: " + response.Content);

			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();

				// Déplace le traitement des données sur un autre thread
				var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent));

				// Console.WriteLine("User: " + result["user"]);

				// Note the change here: we now check the "success" value in the JSON response
				if (result.ContainsKey("success") && (bool)result["success"])
				{
					return true;
				}
				else
				{
					// Optionally, log the error message if present
					if (result.ContainsKey("error"))
					{
						Console.WriteLine("Error from server: " + result["error"]);
						return false;
					}
				}
			}
			else
			{
				// Gérer l'erreur ici
				throw new Exception($"Une erreur est survenue lors de la création de votre compte: {response.StatusCode}");
			}
			return false;
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
				var result = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse));

				// Console.WriteLine("success: " + responseObject["success"]);

				if (result.ContainsKey("success") && (bool)result["success"])
				{
					App.SaveUser(result["user"]);
					Console.WriteLine("*** USER ***: " + result["user"]); // infos du user

					return true;
				}
				else
				{
					// Optionally, log the error message if present
					if (result.ContainsKey("error"))
					{
						Console.WriteLine("Error from server: " + result["error"]);
					}
					return false;
				}
			}
			else
			{
				// Gérer l'erreur ici
				Console.WriteLine("Unsuccessful HTTP response. Status code: " + response.StatusCode);
				return false;
			}
		}

		public void SendConfirmationEmail(string toEmail, string confirmationCode)
		{
			// Créez une instance de client SMTP
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

			// TODO: Securiser le e-mail
			// Utilisez vos informations d'authentification pour le serveur SMTP
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential("apiphare@gmail.com", "cuvwlyybvzinoqcm");
			client.EnableSsl = true;

			// Créez un nouveau message
			MailMessage mailMessage = new MailMessage();

			// Définissez l'expéditeur du message
			mailMessage.From = new MailAddress("apiphare@gmail.com");

			// Ajoutez le destinataire du message
			mailMessage.To.Add(toEmail);

			// Définissez l'objet du message
			mailMessage.Subject = "Email confirmation";

			// Définissez le corps du message
			mailMessage.Body = "Voici votre code de validation a entrer dans votre application pour confirmer votre identite: " + confirmationCode;

			// Envoyez le message
			client.Send(mailMessage);
		}

	}
}
