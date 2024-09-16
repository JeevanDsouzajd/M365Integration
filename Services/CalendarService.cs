using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using O365Poc.Server.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace O365Poc.Server.Services
{
	public class CalendarService
	{
		private readonly HttpClient _httpClient;


		public CalendarService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<EventRQ> AddEventAsync(EventRQ eventRQ)
		{
			return await AddUpdateEventAync(eventRQ, false);
		}

		public async Task<EventRQ> UpdateEventAsync(EventRQ eventRQ)
		{
			return await AddUpdateEventAync(eventRQ, true);
		}

		public async Task<bool> DeleteEventAsync(String id)
		{
			var requestUri = $"https://graph.microsoft.com/v1.0/me/events/{id}";
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ""); // Set AuthToken
			var response = await _httpClient.DeleteAsync(requestUri);
			if (response != null)
			{
				response.EnsureSuccessStatusCode();
				return true;
			}
			return false;
		}

		public async Task<SubscriptionResponse> CreateSubscriptionAsync()
		{
			var response = new SubscriptionResponse();
			var subscription = new
			{
				changeType = "updated",
				notificationUrl = "https://localhost:7248/Calendar/ReceiveWebhook", // add your webhook URL
				resource = "/me/events",
				expirationDateTime = DateTime.UtcNow.AddMinutes(4230).ToString("o"),
				clientState = Convert.ToBase64String(Encoding.UTF8.GetBytes("my-secret-client-state"))
			};

			var content = new StringContent(JsonSerializer.Serialize(subscription), Encoding.UTF8, "application/json");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ""); // Set AuthToken
			var responseData = await _httpClient.PostAsync("https://graph.microsoft.com/v1.0/subscriptions", content);

			responseData.EnsureSuccessStatusCode();

			var responseContent = await responseData.Content.ReadAsStringAsync();

			var subscriptionData = JsonSerializer.Deserialize<SubscriptionResponse>(responseContent);

			return subscriptionData;
		}

		private async Task<EventRQ> AddUpdateEventAync(EventRQ eventRQ, bool isUpdate)
		{
			var requestUri = isUpdate ? $"https://graph.microsoft.com/v1.0/me/events/{eventRQ.id}" : $"https://graph.microsoft.com/v1.0/me/events";

			var requestBody = new Dictionary<string, object>
			{
				{ "subject", eventRQ.subject },
				{ "start", eventRQ.start },
				{ "end", eventRQ.end },
				{ "location", eventRQ.location },
				{ "attendees", eventRQ.attendees }
			};

			// Add the body only if it's not null or empty
			if (!string.IsNullOrEmpty(eventRQ.body?.content))
			{
				requestBody.Add("body", eventRQ.body);
			}

			var jsonString = JsonSerializer.Serialize(requestBody);

			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			content.Headers.ContentType.CharSet = null;
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ""); // Set AuthToken
			var response = isUpdate ? await _httpClient.PatchAsync(requestUri, content) : await _httpClient.PostAsync(requestUri, content);
			var responseData = await response.Content.ReadAsStringAsync();

			response.EnsureSuccessStatusCode();

			var responseContent = JsonSerializer.Deserialize<EventRQ>(responseData);
			return responseContent;

		}
	}
}
