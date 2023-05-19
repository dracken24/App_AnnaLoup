using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoList : ContentPage
	{
		private ObservableCollection<string> tasks;

		public TodoList()
		{
			InitializeComponent();

			Title = "TodoList";

			tasks = new ObservableCollection<string>();
			taskList.ItemsSource = tasks;
		}

		private void OnAddTaskButtonClicked(object sender, EventArgs e)
		{
			var newTask = "- " + newTaskEntry.Text;
			if (!string.IsNullOrWhiteSpace(newTask))
			{
				tasks.Add(newTask);
				newTaskEntry.Text = string.Empty;
			}
		}

		private void OnTaskTapped(object sender, ItemTappedEventArgs e)
		{
			var task = e.Item as string;
			if (tasks.Contains(task))
			{
				tasks.Remove(task);
			}
		}
	}
}
