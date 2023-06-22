using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FindYourPath.DataBase
{
	public class User
	{
		int _id;
		string _username;
		string _email;
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

			// Console.WriteLine("IN USER id: " + _id);
			// Console.WriteLine("IN USER username: " + _username);
			// Console.WriteLine("IN USER email: " + _email);
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
