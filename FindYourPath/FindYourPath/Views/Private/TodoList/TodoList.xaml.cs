using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoList : ContentPage
	{
		private ObservableCollection<TaskList> tasks;

		public class TaskList
		{
			public string Name { get; set; }
			public ObservableCollection<string> SubTasks { get; set; }
		}

		public TodoList()
		{
			InitializeComponent();

			Title = "TodoList";

			tasks = new ObservableCollection<TaskList>();
			taskList.ItemsSource = tasks;
		}

		private void OnAddListButtonClicked(object sender, EventArgs e)
		{
			var newTask = newTaskEntry.Text;
			if (!string.IsNullOrWhiteSpace(newTask))
			{
				tasks.Add(new TaskList()
				{
					Name = newTask,
					SubTasks = new ObservableCollection<string>()
				});
				newTaskEntry.Text = string.Empty;
			}
		}

		private async void OnListTapped(object sender, ItemTappedEventArgs e)
		{
			var taskList = e.Item as TaskList;
			await Navigation.PushAsync(new OneList(taskList));
		}
	}
}
