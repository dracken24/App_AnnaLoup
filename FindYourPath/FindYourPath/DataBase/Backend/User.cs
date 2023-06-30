using Newtonsoft.Json.Linq;
using System;

namespace FindYourPath.DataBase
{
	public class User
	{
		// Informations sur le user
		public int _id
		{
			get;
			set;
		}
		public string _username
		{
			get;
			set;
		}
		public string _name
		{
			get;
			set;
		}
		public string _surname
		{
			get;
			set;
		}
		public string _phone
		{
			get;
			set;
		}
		public string _birthDate
		{
			get;
			set;
		}
		public string _sexe
		{
			get;
			set;
		}
		public string _adress
		{
			get;
			set;
		}
		public string _email
		{
			get;
			set;
		}
		public string _verifEmail
		{
			get;
			set;
		}

		// Privileges du user

		// Peut se connecter apres avoir valider son email
		public bool _canConnect
		{
			get;
			private set;
		}

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

			_id = parsedJson["user"]["User_Id"].Value<int>();
			_username = parsedJson["user"]["username"].Value<string>();
			_email = parsedJson["user"]["8"].Value<string>();
			_name = parsedJson["user"]["Prenom"].Value<string>();
			_surname = parsedJson["user"]["Nom"].Value<string>();
			_phone = parsedJson["user"]["Telephone"].Value<string>();
			_birthDate = parsedJson["user"]["Date_Naissance"].Value<string>();
			_sexe = parsedJson["user"]["Sexe"].Value<string>();
			_adress = parsedJson["user"]["Adresse"].Value<string>();
			_verifEmail = parsedJson["user"]["9"].Value<string>();
			_canConnect = parsedJson["user"]["email_verif"].Value<bool>();

			Console.WriteLine("******************************* In User *****************************");
			// Console.WriteLine(parsedJson["user"]);

			PrintMembers();
		}

		public void PrintMembers()
		{
			Console.WriteLine("IN USER id: " + _id);
			Console.WriteLine("IN USER username: " + _username);
			Console.WriteLine("IN USER email: " + _email);
			Console.WriteLine("IN USER name: " + _name);
			Console.WriteLine("IN USER surname: " + _surname);
			Console.WriteLine("IN USER phone: " + _phone);
			Console.WriteLine("IN USER birthDate: " + _birthDate);
			Console.WriteLine("IN USER sexe: " + _sexe);
			Console.WriteLine("IN USER adress: " + _adress);
			Console.WriteLine("IN USER verifEmail: " + _verifEmail);
			Console.WriteLine("IN USER canConnect: " + (bool)_canConnect);
		}

		public void SetCanConnect(bool canConnect)
		{
			_canConnect = canConnect;
		}

		public bool CheckVerificationCode(string verificationCode)
		{
			if (verificationCode == _verifEmail)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
