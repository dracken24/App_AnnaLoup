using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FindYourPath.Views.TodoList;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OneList : ContentPage
	{
		private ObservableCollection<string> tasks;

		private TaskList _taskList;

		public ObservableCollection<string> Tasks
		{
			get { return tasks; }
			set { tasks = value; }
		}

		public OneList(TaskList taskList)
		{
			InitializeComponent();

			_taskList = taskList;

			Title = "TodoList";

			tasks = taskList.SubTasks;
			taskList.SubTasks = tasks;

			this.BindingContext = this; // Ajoutez cette ligne
		}

		private void OnAddTaskButtonClicked(object sender, EventArgs e)
		{
			var newTask = "- " + newTaskEntry.Text;
			if (!string.IsNullOrWhiteSpace(newTask))
			{
				Tasks.Add(newTask);
				newTaskEntry.Text = string.Empty;
			}
		}

		private void OnTaskTapped(object sender, ItemTappedEventArgs e)
		{
			var task = e.Item as string;
			if (Tasks.Contains(task))
			{
				Tasks.Remove(task);
			}
		}
	}
}