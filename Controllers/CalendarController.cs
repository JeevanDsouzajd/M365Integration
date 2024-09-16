using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using O365Poc.Server.Models;
using O365Poc.Server.Services;
using System.Net.Http;

namespace O365Poc.Server.Controllers
{
	[Route("Calendar")]
	[ApiController]
	public class CalendarController : ControllerBase
	{

		private readonly CalendarService calendarService;
		private readonly ILogger<OneDriveController> _logger;

		public CalendarController(CalendarService calendarService, ILogger<OneDriveController> logger)
		{
			this.calendarService = calendarService;
			_logger = logger;
		}

		[HttpGet("")]
		public string Index()
		{
			return "Outlook Integration";
		}

		[HttpPost("CreateEvent")]
		public async Task<EventRQ> AddEventAsync([FromBody] EventRQ eventRQ)
		{
			try
			{
				var response = await calendarService.AddEventAsync(eventRQ);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error adding event: {ex.ToString()}");
				return null;
			}
		}

		[HttpPatch("UpdateEvent")]
		public async Task<EventRQ> UpdateEventAsync([FromBody] EventRQ eventRQ)
		{
			try
			{
				var response = await calendarService.UpdateEventAsync(eventRQ);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error updating event: {ex.ToString()}");
				return null;
			}
		}

		[HttpDelete("DeleteEvent")]
		public async Task<bool> DeleteEventAsync([FromQuery] String id)
		{
			try
			{
				var response = await calendarService.DeleteEventAsync(id);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error deleting event: {ex.ToString()}");
				return false;
			}
		}
	}
}
