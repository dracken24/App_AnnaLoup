using Newtonsoft.Json.Linq;
using System;

namespace FindYourPath.DataBase
{
	public class User
	{
		// Informations sur le user
		int _id;
		string _username;
		string _email;

		// Privileges du user
		bool _canConnect = false; // Peut se connecter apres avoir valider son email

		// Roles du user pour le futur chat
		bool _isAdmin = false;
		bool _isModerator = false;

		public User()
		{

		}

		public void SaveUser(object user)
		{
			Console.WriteLine("IN USER: " + user);
			string json = user.ToString();
			JObject parsedJson = JObject.Parse(json);

			_id = parsedJson["user"]["id"].Value<int>();
			_username = parsedJson["user"]["username"].Value<string>();
			_email = parsedJson["user"]["email"].Value<string>();

			// PrintMembers();
		}

		public void PrintMembers()
		{
			Console.WriteLine("IN USER id: " + _id);
			Console.WriteLine("IN USER username: " + _username);
			Console.WriteLine("IN USER email: " + _email);
		}

		public int GetUserId()
		{
			return _id;
		}
	}
}
