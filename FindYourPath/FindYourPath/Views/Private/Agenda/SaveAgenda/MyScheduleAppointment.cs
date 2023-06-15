using SQLite;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindYourPath.Views.Private.Agenda.SaveAgenda
{
	public class MyScheduleAppointment : ScheduleAppointment
	{
		[PrimaryKey, AutoIncrement]
		public int MyId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public TimeSpan MyStartTime { get; set; }
		public TimeSpan MyEndTime { get; set; }
		public string MyLocation { get; set; }
		public int UserId { get; set; }
		
		/*
		[PrimaryKey, AutoIncrement]
		public int MyId { get; set; }
		public string Description { get; set; }
		public int UserId { get; set; }
		*/
		
		// Vos autres propriétés personnalisées...
		
	}
}
