using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O365Poc.Server.Models;
using O365Poc.Server.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace O365Poc.Server.Controllers
{
	[Route("OneDrive")]
	[ApiController]
	public class OneDriveController : ControllerBase
	{
		private readonly ILogger<OneDriveController> _logger;
		private readonly OneDriveService _oneDriveService;
		private readonly HttpClient _httpClient;

		public OneDriveController(ILogger<OneDriveController> logger, IHttpClientFactory httpClientFactory, OneDriveService oneDriveService)
		{
			_logger = logger;
			_oneDriveService = oneDriveService;
			_httpClient = httpClientFactory.CreateClient();
		}

		string tokenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "AzureToken.json");

		[HttpGet("")]
		public string Index()
		{
			return "One Drive Integration";
		}

		private string SetAuthorizationHeader()
		{
			string fileContent = System.IO.File.ReadAllText(tokenFilePath);
			var tokens = JsonSerializer.Deserialize<TokenResponse>(fileContent);
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.access_token?.ToString());
			return tokens.access_token;
		}

		[HttpGet("Files")]
		public async Task<IActionResult> GetFiles()
		{
			try
			{
				SetAuthorizationHeader();

				var response = await _oneDriveService.GetOnedriveFiles(_httpClient);

				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error calling Graph API: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}
		}

		[HttpGet("Drive")]
		public async Task<IActionResult> GetDriveById([FromQuery] string fileId)
		{
			try
			{
				SetAuthorizationHeader();

				var response = await _oneDriveService.GetDriveById(_httpClient, fileId);

				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error downloading file: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}
		}

		[HttpPost("Download")]
		public async Task<IActionResult> DownloadFile([FromQuery] string fileId)
		{
			try
			{
				SetAuthorizationHeader();

				var downloadLink = await _oneDriveService.GetDownloadLink(_httpClient, fileId);

				if (String.IsNullOrEmpty(downloadLink))
				{
					return BadRequest("Invalid File");
				}

				var response = await _httpClient.GetAsync(downloadLink);
				var fileStream = await response.Content.ReadAsStreamAsync();

				return File(fileStream, response.Content.Headers.ContentType.ToString(), "fileName");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error downloading file: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}
		}

		[HttpPost("Subscription")]
		public async Task<IActionResult> CreateSubscriptionAsync()
		{
			try
			{
				SetAuthorizationHeader();

				var response = await _oneDriveService.CreateSubscriptionAsync(_httpClient);
				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error downloading file: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}

		}

		[HttpPost("ReceiveWebhook")]
		public async Task<IActionResult> ReceiveNotification()
		{
			SetAuthorizationHeader();
			const string ValidationTokenKey = "validationToken";
			if (Request.Query.ContainsKey(ValidationTokenKey))
			{
				string token = Request.Query[ValidationTokenKey];
				return Content(token, "text/plain");
			}

			try
			{
				var notifications = await ParseIncomingNotificationAsync();
				if (notifications != null && notifications.Any())
				{
					var content = await _oneDriveService.TrackChanges(_httpClient);
					_logger.LogInformation($"Processed content: {content}");

					string requestBody;
					using (var reader = new StreamReader(Request.Body))
					{
						requestBody = await reader.ReadToEndAsync();
					}

					return Ok(new { Message = "Notification processed successfully", Content = content });
				}
				return BadRequest("Notification payload is null.");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error processing notification: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}
		}

		[HttpPost("UploadToOneDrive")]
		public async Task<IActionResult> UploadFileToOneDrive(IFormFile file, [FromQuery] string parentId = "root")
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded.");
			}

			try
			{
				SetAuthorizationHeader();

				var response = await _oneDriveService.UploadFileToOneDrive(_httpClient, file, parentId);

				return Ok(new { Message = "File uploaded successfully", Response = response });
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error uploading file to OneDrive: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}
		}

		[HttpPost("UpdateFileContent")]
		public async Task<IActionResult> UpdateFileContentOnOneDrive(IFormFile file, [FromQuery] string fileId)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded.");
			}

			if (string.IsNullOrEmpty(fileId))
			{
				return BadRequest("File ID is required.");
			}

			try
			{
				SetAuthorizationHeader();

				var response = await _oneDriveService.UpdateFileContentOnOneDrive(_httpClient, fileId, file);

				return Ok(new { Message = "File content updated successfully", Response = response });
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error updating file content on OneDrive: {ex.ToString()}");
				return StatusCode(500, new { Error = "Internal server error", Details = ex.ToString() });
			}
		}

		private async Task<OneDriveWebhookNotification[]> ParseIncomingNotificationAsync()
		{
			try
			{
				using (var reader = new StreamReader(Request.Body))
				{
					var jsonContent = await reader.ReadToEndAsync();
					var collection = JsonSerializer.Deserialize<OneDriveNotificationCollection>(jsonContent);
					if (collection?.Notifications != null)
					{
						return collection.Notifications;
					}
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
