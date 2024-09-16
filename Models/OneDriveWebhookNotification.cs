using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class OneDriveWebhookNotification
	{
		[JsonPropertyName("clientState")]
		public string ClientState { get; set; }
		[JsonPropertyName("resource")]
		public string Resource { get; set; }
		[JsonPropertyName("subscriptionExpirationDateTime")]
		public string SubscriptionExpirationDateTime { get; set; }
		[JsonPropertyName("subscriptionId")]
		public string SubscriptionId { get; set; }
		[JsonPropertyName("receivedDateTime")]
		public DateTime ReceivedDateTime { get; set; }
		public OneDriveWebhookNotification()
		{
			this.ReceivedDateTime = DateTime.UtcNow;
		}
	}
}
