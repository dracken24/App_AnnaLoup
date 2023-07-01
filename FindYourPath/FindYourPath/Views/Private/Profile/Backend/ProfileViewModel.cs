using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FindYourPath.Views.Private.Profile
{
	public class ProfileViewModel : INotifyPropertyChanged
	{
		private string firstName;
		private string name;
		private string address;
		private string email;
		private string phone;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public string FirstName
		{
			get => firstName;
			set
			{
				firstName = value;
				OnPropertyChanged();
			}
		}

		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged();
			}
		}

		public string Address
		{
			get => address;
			set
			{
				address = value;
				OnPropertyChanged();
			}
		}

		public string Email
		{
			get => email;
			set
			{
				email = value;
				OnPropertyChanged();
			}
		}

		public string Phone
		{
			get => phone;
			set
			{
				phone = value;
				OnPropertyChanged();
			}
		}
	}
}
