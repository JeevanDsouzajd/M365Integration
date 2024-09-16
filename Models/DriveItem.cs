using System.Text.Json;
using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class DriveItem
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("@microsoft.graph.downloadUrl")]
		public string DownloadUrl { get; set; }

		[JsonExtensionData]
		public Dictionary<string, object> AdditionalData { get; set; }
	}
}
