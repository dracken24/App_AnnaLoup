using FindYourPath.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using XCalendar.Models; // Assurez-vous que c'est le bon espace de nom pour CalendarDay

namespace FindYourPath.Converters
{
	namespace FindYourPath.Converters
	{
		public class CalendarDayToEventsConverter : IValueConverter
		{
			private readonly Func<DateTime, List<MyScheduleAppointment>> _getEventsForDay;

			public CalendarDayToEventsConverter(Func<DateTime, List<MyScheduleAppointment>> getEventsForDay)
			{
				_getEventsForDay = getEventsForDay;
			}

			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				var calendarDay = value as CalendarDay;
				if (calendarDay == null)
					return null;

				return _getEventsForDay(calendarDay.DateTime.Date);
			}

			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new NotImplementedException();
			}
		}
	}
}
