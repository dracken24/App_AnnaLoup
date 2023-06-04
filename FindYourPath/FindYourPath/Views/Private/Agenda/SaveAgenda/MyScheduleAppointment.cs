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

		// Vos autres propriétés personnalisées...
	}
}
