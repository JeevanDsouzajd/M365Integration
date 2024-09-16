using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class OneDriveNotificationCollection
	{
		[JsonPropertyName("value")]
		public OneDriveWebhookNotification[] Notifications { get; set; }
	}
}
