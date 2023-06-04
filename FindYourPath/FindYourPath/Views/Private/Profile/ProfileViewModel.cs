using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FindYourPath.Views.Private.Profile
{
	public class ProfileViewModel : INotifyPropertyChanged
	{
		private string name;
		private string address;
		private string email;
		private string phone;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged();
				//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
			}
		}

		public string Address
		{
			get => address;
			set
			{
				address = value;
				OnPropertyChanged();
				//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
			}
		}

		public string Email
		{
			get => email;
			set
			{
				email = value;
				OnPropertyChanged();
				//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
			}
		}

		public string Phone
		{
			get => phone;
			set
			{
				phone = value;
				OnPropertyChanged();
				//PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
			}
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}

}
