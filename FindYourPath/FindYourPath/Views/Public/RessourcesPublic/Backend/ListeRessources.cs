using System;
using System.Collections.Generic;
using System.Text;

namespace FindYourPath.Views
{
    class ListeRessources
    {
		public string Name { get; set; }
		public string City { get; set; }
		public Dictionary<string, OneRessource> Options { get; set; }
	}
}
