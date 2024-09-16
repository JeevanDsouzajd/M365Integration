using System.Net.Http.Headers;
using O365Poc.Server.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace O365Poc.Server.Services
{
	public class OneDriveService
	{
		public async Task<OneDriveFilesRS> GetOnedriveFiles(HttpClient httpClient)
		{
			var requestUri = "https://graph.microsoft.com/v1.0/me/drive/root/children";
			var response = await httpClient.GetAsync(requestUri);

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var filesData = JsonSerializer.Deserialize<OneDriveFilesRS>(content);
			return filesData;
		}

		public async Task<OneDriveFilesRS> GetDriveById(HttpClient httpClient, string fileId)
		{
			var requestUri = $"https://graph.microsoft.com/v1.0/me/drive/items/{fileId}/children";
			var response = await httpClient.GetAsync(requestUri);

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var fileData = JsonSerializer.Deserialize<OneDriveFilesRS>(content);
			return fileData;
		}

		public async Task<TrackChangesRS> TrackChanges(HttpClient httpClient)
		{
			var requestUri = $"https://graph.microsoft.com/v1.0/me/drive/root/delta";


			var response = await httpClient.GetAsync(requestUri);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var changes = JsonSerializer.Deserialize<TrackChangesRS>(content);

			return changes;
		}

		public async Task<string> GetDownloadLink(HttpClient httpClient, string fileId)
		{
			var requestUri = $"https://graph.microsoft.com/v1.0/me/drive/items/{fileId}";
			var response = await httpClient.GetAsync(requestUri);

			response.EnsureSuccessStatusCode();

			var contentString = await response.Content.ReadAsStringAsync();
			var driveItem = JsonSerializer.Deserialize<DriveItem>(contentString);

			if(driveItem.DownloadUrl == null)
			{
				return null;
			}

			var downloadUrl = driveItem.DownloadUrl.ToString();

			return downloadUrl;
		}


		public async Task<FileRS> UploadFileToOneDrive(HttpClient httpClient, IFormFile file, string parentId = "root")
		{
			var uploadUrl = $"https://graph.microsoft.com/v1.0/me/drive/items/{parentId}:/{file.FileName}:/content";

			using (var content = new StreamContent(file.OpenReadStream()))
			{
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
				var response = await httpClient.PutAsync(uploadUrl, content);

				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync();

				var fileData = JsonSerializer.Deserialize<FileRS>(responseBody);

				return fileData;
			}
		}

		public async Task<FileRS> UpdateFileContentOnOneDrive(HttpClient httpClient, string fileId, IFormFile file)
		{
			var updateUrl = $"https://graph.microsoft.com/v1.0/me/drive/items/{fileId}/content";

			using (var content = new StreamContent(file.OpenReadStream()))
			{
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
				var response = await httpClient.PutAsync(updateUrl, content);

				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync();

				var fileData = JsonSerializer.Deserialize<FileRS>(responseBody);
				return fileData;
			}
		}

		public async Task<SubscriptionResponse> CreateSubscriptionAsync(HttpClient httpClient)
		{
			var response = new SubscriptionResponse();
			var subscription = new
			{
				changeType = "updated",
				notificationUrl = "https://bwpoc.azurewebsites.net/OneDrive/ReceiveWebhook",
				resource = "/me/drive/root",
				expirationDateTime = DateTime.UtcNow.AddMinutes(4230).ToString("o"),
				clientState = Convert.ToBase64String(Encoding.UTF8.GetBytes("my-secret-client-state"))
			};

			var content = new StringContent(JsonSerializer.Serialize(subscription), Encoding.UTF8, "application/json");
			var responseData = await httpClient.PostAsync("https://graph.microsoft.com/v1.0/subscriptions", content);

			responseData.EnsureSuccessStatusCode();

			var responseContent = await responseData.Content.ReadAsStringAsync();

			var subscriptionData = JsonSerializer.Deserialize<SubscriptionResponse>(responseContent);

			return subscriptionData;
		}
	}
}
