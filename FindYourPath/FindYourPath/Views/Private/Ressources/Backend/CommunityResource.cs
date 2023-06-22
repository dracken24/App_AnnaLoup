using System;
using System.Collections.Generic;
using System.Text;

namespace FindYourPath.Views
{
	public class CommunityResource
	{
		public string Name
		{ 
			get; set;
		}
		public string Address
		{
			get; set;
		}
		public string PhoneNumber
		{
			get; set;
		}
		public string Url
		{
			get; set;
		}
		public string Type
		{
			get; set;
		}

		public override string ToString()
		{
			return $"{Name}, {Address}, {PhoneNumber}, {Type}";
		}
	}
}
