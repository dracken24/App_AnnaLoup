using SQLite;
using System;
using Xamarin.Forms;
using XCalendar.Models;

namespace FindYourPath.Views
{
	public class Event : BaseObservableModel
	{
		[PrimaryKey, AutoIncrement]
		public int MyId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public string Location { get; set; }
		public int UserId { get; set; }
		public Color Color { get; set; } = Color.Blue;

		public string PrintMembers()
		{
			return $"Title: {Title}, Description: {Description}, StartDate: {StartDate}, EndDate: {EndDate}, StartTime: {StartTime}, EndTime: {EndTime}, Location: {Location}, ";
		}
	}

	
}
