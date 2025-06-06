﻿using FindYourPath.Views.Private.Agenda.SaveAgenda;
using Google.Apis.Calendar.v3.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XCalendar.Core.Collections;
using XCalendar.Core.Enums;
using XCalendar.Core.Extensions;
using XCalendar.Core.Models;

namespace FindYourPath.Views
{
	public class EventCalendarViewModel : BaseViewModel
	{
		#region Properties
		public Calendar<EventDay> EventCalendar { get; set; } = new Calendar<EventDay>()
		{
			SelectedDates = new ObservableRangeCollection<DateTime>(),
			SelectionAction = SelectionAction.Modify,
			SelectionType = SelectionType.Single
		};

		// public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>();
		public ObservableRangeCollection<MyEvent> SelectedEvents { get; } = new ObservableRangeCollection<MyEvent>();
		private EventService _eventService;
		private Page _page;
		private DateTime _selectedDate;

		#endregion

		#region Commands
		public ICommand NavigateCalendarCommand { get; set; }
		public ICommand ChangeDateSelectionCommand { get; set; }
		#endregion

		#region Constructors
		public EventCalendarViewModel(Page page, EventService eventService)
		{
			_eventService = eventService;
			NavigateCalendarCommand = new Command<int>(NavigateCalendar);
			ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);

			_selectedDate = DateTime.Now;
			_page = page;


			EventCalendar.SelectedDates.CollectionChanged += SelectedDates_CollectionChanged;
			EventCalendar.DaysUpdated += EventCalendar_DaysUpdated;
			foreach (var day in EventCalendar.Days)
			{
				day.Events.ReplaceRange(App.Events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}

		}
		#endregion

		#region Methods
		public void EventCalendar_DaysUpdated(object sender, EventArgs e)
		{
			Console.WriteLine("Days updated");
			if (EventCalendar.SelectedDates.Count == 0)
			{
				_selectedDate = DateTime.Today;
				return;
			}
			foreach (var day in EventCalendar.Days)
			{
				day.Events.ReplaceRange(App.Events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}
		}

		private void OnMonthChanged(object sender, EventArgs e)
		{
			Console.WriteLine("Month changed");
			foreach (var day in EventCalendar.Days)
			{
				day.Events.ReplaceRange(App.Events.Where(x => x.StartDate.Date == day.DateTime.Date));
			}
		}

		public void SelectedDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Console.WriteLine("Selected dates changed");
			SelectedEvents.ReplaceRange(App.Events.Where(x => EventCalendar.SelectedDates.Any(y => x.StartDate.Date == y.Date)).OrderByDescending(x => x.StartDate));
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

			// Call OnMonthChanged after navigating
			OnMonthChanged(this, EventArgs.Empty);
		}


		public void ChangeDateSelection(DateTime dateTime) 
		{
			_selectedDate = dateTime;
			Console.WriteLine("Change date selection");
			EventCalendar?.ChangeDateSelection(dateTime);
		}
		#endregion

		//**********************************************************************************************************************

		void UpdateSelectedEvent()
		{
			ObservableRangeCollection<MyEvent> tmpsEvents;
			tmpsEvents = new ObservableRangeCollection<MyEvent>(App.Events.Where(x => x.StartDate.Date == _selectedDate.Date).OrderByDescending(x => x.StartDate));
			SelectedEvents.ReplaceRange(tmpsEvents);
		}

		public async Task OnAddEventButtonClicked(object sender, EventArgs e, Agenda agenda)
		{
			DateTime selectedDate = _selectedDate;

			await _page.Navigation.PushAsync(new AddEventPage(selectedDate, _eventService, App.User, agenda));
			UpdateSelectedEvent();
		}

		public async Task Initialize()
		{
			Console.WriteLine("Initialize");
			try
			{
				await Task.Run(() => LoadEventsFromDatabase(App.User._id));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}

		private async Task LoadEventsFromDatabase(int userId)
		{
			App.Events.Clear();

			if (_eventService == null)
			{
				Console.WriteLine("eventService is null");
				return;
			}

			await _eventService.GetEventsAsync(userId);
			if (App.Events == null)
			{
				Console.WriteLine("GetEventsAsync returned null");
				return;
			}
		}
		//**********************************************************************************************************************
	}
}
