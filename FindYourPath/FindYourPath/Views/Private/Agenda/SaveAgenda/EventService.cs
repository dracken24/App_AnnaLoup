using Google.Apis.Calendar.v3.Data;
using SQLite;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindYourPath.Views.Private.Agenda.SaveAgenda
{
	public class EventService
	{
		readonly SQLiteAsyncConnection _database;

		public EventService(string dbPath)
		{
			_database = new SQLiteAsyncConnection(dbPath);
			//_database.CreateTableAsync<MyScheduleAppointment>().Wait();
		}

		public async Task InitializeAsync()
		{
			await _database.CreateTableAsync<Event>();
		}

		public Task<List<MyScheduleAppointment>> GetEventsAsync()
		{
			return _database.Table<MyScheduleAppointment>().ToListAsync();
		}

		public Task<int> SaveEventAsync(MyScheduleAppointment appointment)
		{
			if (appointment.MyId != 0)
			{
				return _database.UpdateAsync(appointment);
			}
			else
			{
				return _database.InsertAsync(appointment);
			}
		}
	}
}
