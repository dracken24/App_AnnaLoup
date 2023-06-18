using System;
using System.Collections.Generic;
using XCalendar.Core.Models;

namespace FindYourPath.Views
{
	public class DayModel
	{
		public DateTime Date { get; set; }
		public List<MyScheduleAppointment> Events { get; set; } = new List<MyScheduleAppointment>();
	}
}
