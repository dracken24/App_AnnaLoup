using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Threading.Tasks;

namespace FindYourPath.Services
{
	// TODO: Class pour communication futur avec Google calandar
	public partial class AgendaService
	{
		private static string[] Scopes = { CalendarService.Scope.Calendar };
		private static string ApplicationName = "FindYourPath";
		private CalendarService _service;

		public AgendaService(string apiKey)
		{
			_service = new CalendarService(new BaseClientService.Initializer()
			{
				ApplicationName = ApplicationName,
				ApiKey = apiKey,
			});
		}

		public async Task AddEvent(string calendarId, Event newEvent)
		{
			await _service.Events.Insert(newEvent, calendarId).ExecuteAsync();
		}

		public Event CreateEvent(DateTime start, DateTime end, string summary, string description)
		{
			Event newEvent = new Event
			{
				Summary = summary,
				Location = "", // if needed
				Description = description,
				Start = new EventDateTime()
				{
					DateTime = start,
					TimeZone = "America/Los_Angeles", // change to your timezone
				},
				End = new EventDateTime()
				{
					DateTime = end,
					TimeZone = "America/Los_Angeles", // change to your timezone
				},
			};

			return newEvent;
		}

		public ScheduleAppointment ConvertGoogleEventToScheduleAppointment(Google.Apis.Calendar.v3.Data.Event googleEvent)
		{
			// Création d'un nouvel événement ScheduleAppointment
			ScheduleAppointment newEvent = new ScheduleAppointment()
			{
				Subject = googleEvent.Summary,
				StartTime = Convert.ToDateTime(googleEvent.Start.DateTimeRaw),
				EndTime = Convert.ToDateTime(googleEvent.End.DateTimeRaw),
				Location = googleEvent.Location,
				Notes = googleEvent.Description,
			};

			return newEvent;
		}

	}
}

/*
Notez que cette classe utilise une clé API pour l'authentification.
Si vous prévoyez d'utiliser OAuth 2.0 pour l'authentification, vous devrez modifier cette partie du code.
*/
