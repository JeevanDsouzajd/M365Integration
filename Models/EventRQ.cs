using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class EventRQ
	{
		[JsonPropertyName("@odata.context")]
		public string oDataContext { get; set; }
		[JsonPropertyName("@odata.etag")]
		public string oDataEtag { get; set; }
		public string id { get; set; }
		public DateTime createdDateTime { get; set; }
		public DateTime lastModifiedDateTime { get; set; }
		public string changeKey { get; set; }
		public List<string> categories { get; set; }
		public string transactionId { get; set; }
		public string originalStartTimeZone { get; set; }
		public string originalEndTimeZone { get; set; }
		public string iCalUId { get; set; }
		public string uid { get; set; }
		public int reminderMinutesBeforeStart { get; set; }
		public bool isReminderOn { get; set; }
		public bool hasAttachments { get; set; }
		public string subject { get; set; }
		public string bodyPreview { get; set; }
		public string importance { get; set; }
		public string sensitivity { get; set; }
		public bool isAllDay { get; set; }
		public bool isCancelled { get; set; }
		public bool isOrganizer { get; set; }
		public bool responseRequested { get; set; }
		public string seriesMasterId { get; set; }
		public string showAs { get; set; }
		public string type { get; set; }
		public string webLink { get; set; }
		public string onlineMeetingUrl { get; set; }
		public bool isOnlineMeeting { get; set; }
		public string onlineMeetingProvider { get; set; }
		public bool allowNewTimeProposals { get; set; }
		public string occurrenceId { get; set; }
		public bool isDraft { get; set; }
		public bool hideAttendees { get; set; }
		public ResponseStatus responseStatus { get; set; }
		public Body body { get; set; }
		public DateTimeTimeZone start { get; set; }
		public DateTimeTimeZone end { get; set; }
		public Location location { get; set; }
		public List<Location> locations { get; set; }
		public Recurrence recurrence { get; set; }
		public List<Attendee> attendees { get; set; }
		public Organizer organizer { get; set; }
		public OnlineMeeting onlineMeeting { get; set; }
	}

	public class ResponseStatus
	{
		public string response { get; set; }
		public DateTime time { get; set; }
	}

	public class Body
	{
		public string contentType { get; set; }
		public string content { get; set; }
	}

	public class DateTimeTimeZone
	{
		public string dateTime { get; set; }
		public string timeZone { get; set; }
	}

	public class Location
	{
		public string displayName { get; set; }
		public string locationType { get; set; }
		public string uniqueId { get; set; }
		public string uniqueIdType { get; set; }
	}

	public class Attendee
	{
		public string type { get; set; }
		public Status status { get; set; }
		public EmailAddress emailAddress { get; set; }
	}

	public class Status
	{
		public string response { get; set; }
		public DateTime time { get; set; }
	}

	public class EmailAddress
	{
		public string name { get; set; }
		public string address { get; set; }
	}

	public class Organizer
	{
		public EmailAddress emailAddress { get; set; }
	}

	public class OnlineMeeting
	{
		public string joinUrl { get; set; }
	}

	public class Recurrence
	{
		public Pattern pattern { get; set; }
		public Range range { get; set; }
	}

	public class Pattern
	{
		public string type { get; set; }
		public int interval { get; set; }
		public List<string> daysOfWeek { get; set; }
	}

	public class Range
	{
		public string type { get; set; }
		public string startDate { get; set; }
		public string endDate { get; set; }
	}


}

