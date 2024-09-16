using System.Text.Json.Serialization;
using static System.Environment;

namespace O365Poc.Server.Models
{
	public class TrackChangesRS
	{
		[JsonPropertyName("@odata.context")]
		public string DataContext { get; set; }
		[JsonPropertyName("@odata.deltaLink")]
		public string DataDeltaLink { get; set; }
		[JsonPropertyName("value")]
		public List<FileRS> Value { get; set; }
	}

	public class TrackChangesItem
	{
		[JsonPropertyName("@odata.type")]
		public string OdataType { get; set; }

		[JsonPropertyName("@microsoft.graph.Decorator")]
		public string MicrosoftGraphDecorator { get; set; }

		[JsonPropertyName("createdBy")]
		public CreatedBy CreatedBy { get; set; }

		[JsonPropertyName("createdDateTime")]
		public DateTime CreatedDateTime { get; set; }

		[JsonPropertyName("eTag")]
		public string ETag { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("lastModifiedBy")]
		public LastModifiedBy LastModifiedBy { get; set; }

		[JsonPropertyName("lastModifiedDateTime")]
		public DateTime LastModifiedDateTime { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("parentReference")]
		public ParentReference ParentReference { get; set; }

		[JsonPropertyName("webUrl")]
		public string WebUrl { get; set; }

		[JsonPropertyName("cTag")]
		public string CTag { get; set; }

		[JsonPropertyName("fileSystemInfo")]
		public FileSystemInfo FileSystemInfo { get; set; }

		[JsonPropertyName("folder")]
		public Folder Folder { get; set; }

		[JsonPropertyName("size")]
		public long Size { get; set; }

		[JsonPropertyName("shared")]
		public Shared Shared { get; set; }
	}
}
