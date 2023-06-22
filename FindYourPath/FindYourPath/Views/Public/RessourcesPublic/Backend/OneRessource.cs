using System;
using System.Collections.Generic;
using System.Text;

namespace FindYourPath.Views
{
	public class OneRessource
	{
		public string Name
		{
			get; set;
		}
		public string Adress
		{
			get; set;
		}
		public string Phone
		{
			get; set;
		}
		public string Url
		{
			get; set;
		}
		public string Description
		{
			get; set;
		}

		public override string ToString()
		{
			return $"{Name}, {Phone}";
		}
	}
}
