using FindYourPath.DataBase;
using FindYourPath.Views.Private.Agenda.SaveAgenda;
using Google.Apis.Calendar.v3.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XCalendar.Core.Collections;
using XCalendar.Core.Enums;
using XCalendar.Core.Extensions;
using XCalendar.Core.Models;
//using Xamarin.CommunityToolkit.ObjectModel;


namespace FindYourPath.Views
{
	public class EventCalendarExampleViewModel : BaseViewModel
	{
		#region Properties
		public Calendar<EventDay> EventCalendar { get; set; } = new Calendar<EventDay>()
		{
			SelectedDates = new ObservableRangeCollection<DateTime>(),
			SelectionAction = SelectionAction.Modify,
			SelectionType = SelectionType.Single
		};

		public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>();
		public ObservableRangeCollection<Event> SelectedEvents { get; } = new ObservableRangeCollection<Event>();
		private EventService eventService;
		private Page _page;
		private DateTime _selectedDate;

		#endregion

		#region Commands
		public ICommand NavigateCalendarCommand { get; set; }
		public ICommand ChangeDateSelectionCommand { get; set; }
		#endregion

		#region Constructors
		public EventCalendarExampleViewModel(Page page)
		{
			eventService = new EventService(App.ConnectionString);
			NavigateCalendarCommand = new Command<int>(NavigateCalendar);
			ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);

			_selectedDate = DateTime.Now;
			_page = page;


			EventCalendar.SelectedDates.CollectionChanged += SelectedDates_CollectionChanged;
			EventCalendar.DaysUpdated += EventCalendar_DaysUpdated;
			foreach (var day in EventCalendar.Days)
			{
				day.Events.ReplaceRange(Events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}

		}
		#endregion

		#region Methods
		private void EventCalendar_DaysUpdated(object sender, EventArgs e)
		{
			Console.WriteLine("Days updated");
			if (EventCalendar.SelectedDates.Count == 0)
			{
				_selectedDate = DateTime.Today;
				return;
			}
			foreach (var day in EventCalendar.Days)
			{
				day.Events.ReplaceRange(Events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}
		}
		private void SelectedDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Console.WriteLine("Selected dates changed");
			SelectedEvents.ReplaceRange(Events.Where(x => EventCalendar.SelectedDates.Any(y => x.StartDate.Date == y.Date)).OrderByDescending(x => x.StartDate));
		}
		public void NavigateCalendar(int amount)
		{
			Console.WriteLine("Navigate calendar");
			if (EventCalendar.NavigatedDate.TryAddMonths(amount, out DateTime targetDate))
			{
				EventCalendar.Navigate(targetDate - EventCalendar.NavigatedDate);
			}
			else
			{
				EventCalendar.Navigate(amount > 0 ? TimeSpan.MaxValue : TimeSpan.MinValue);
			}
		}
		public void ChangeDateSelection(DateTime dateTime) 
		{
			_selectedDate = dateTime;
			Console.WriteLine("Change date selection");
			EventCalendar?.ChangeDateSelection(dateTime);
		}
		#endregion

		//**********************************************************************************************************************

		public async void OnAddEventButtonClicked(object sender, EventArgs e)
		{
			DateTime selectedDate = _selectedDate;

			User user = App.User;
			_page.Navigation.PushAsync(new AddEventPage(Events, selectedDate, eventService, user));
		}

		public async Task<ObservableRangeCollection<Event>> Initialize()
		{
			try
			{
				return await Task.Run(() => LoadEventsFromDatabase(App.User.GetUserId()));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
				return new ObservableRangeCollection<Event>();
			}
		}

		private async Task<ObservableRangeCollection<Event>> LoadEventsFromDatabase(int userId)
		{
			Events.Clear();

			if (eventService == null)
			{
				Console.WriteLine("eventService is null");
				return null;
			}

			var events = await eventService.GetEventsAsync(userId);
			if (events == null)
			{
				Console.WriteLine("GetEventsAsync returned null");
				return null;
			}

			foreach (var e in events)
			{
				Events.Add(e);
			}

			return Events;
		}
		//**********************************************************************************************************************
	}
}
