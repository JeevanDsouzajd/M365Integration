using System.Text.Json;
using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class SubscriptionResponse
	{
		[JsonPropertyName("@odata.context")]
		public string ODataContext { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("resource")]
		public string Resource { get; set; }

		[JsonPropertyName("applicationId")]
		public string ApplicationId { get; set; }

		[JsonPropertyName("changeType")]
		public string ChangeType { get; set; }

		[JsonPropertyName("clientState")]
		public string ClientState { get; set; }

		[JsonPropertyName("notificationUrl")]
		public string NotificationUrl { get; set; }

		[JsonPropertyName("notificationQueryOptions")]
		public string NotificationQueryOptions { get; set; }

		[JsonPropertyName("lifecycleNotificationUrl")]
		public string LifecycleNotificationUrl { get; set; }

		[JsonPropertyName("expirationDateTime")]
		public DateTime ExpirationDateTime { get; set; }

		[JsonPropertyName("creatorId")]
		public string CreatorId { get; set; }

		[JsonPropertyName("includeResourceData")]
		public string IncludeResourceData { get; set; }

		[JsonPropertyName("latestSupportedTlsVersion")]
		public string LatestSupportedTlsVersion { get; set; }

		[JsonPropertyName("encryptionCertificate")]
		public string EncryptionCertificate { get; set; }

		[JsonPropertyName("encryptionCertificateId")]
		public string EncryptionCertificateId { get; set; }

		[JsonPropertyName("notificationUrlAppId")]
		public string NotificationUrlAppId { get; set; }
	}
}
