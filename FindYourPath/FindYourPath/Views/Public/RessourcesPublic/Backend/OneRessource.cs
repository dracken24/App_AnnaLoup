using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindYourPath.Views
{
	public class OneRessource
	{
		[PrimaryKey, AutoIncrement]
		public int id
		{
			get; set;
		}

		public string Nom
		{
			get; set;
		}

		public string Adresse
		{
			get; set;
		}
		
		public string Telephone
		{
			get; set;
		}

		public string URL
		{
			get; set;
		}

		public string Type
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public int User_Id
		{
			get; set;
		}

		public override string ToString()
		{
			return $"{Nom}, {Telephone}";
		}
	}
}
