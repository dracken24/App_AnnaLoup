using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FindYourPath.DataBase
{
	// Class pour linker les vars du login.xamel aux vars dans le c#
	public class LoginPageViewModel : INotifyPropertyChanged
	{
		private string _username;
		private string _password;

		public string Username
		{
			get { return _username; }
			set
			{
				_username = value;
				OnPropertyChanged();
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				OnPropertyChanged();
			}
		}

		// Implement INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

}
