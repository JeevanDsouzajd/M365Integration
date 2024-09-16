using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class FileRS
	{
		[JsonPropertyName("@odata.context")]
		public string ODataContext { get; set; }

		[JsonPropertyName("@microsoft.graph.downloadUrl")]
		public string DownloadUrl { get; set; }

		[JsonPropertyName("createdDateTime")]
		public DateTime CreatedDateTime { get; set; }

		[JsonPropertyName("eTag")]
		public string ETag { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("lastModifiedDateTime")]
		public DateTime LastModifiedDateTime { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("webUrl")]
		public string WebUrl { get; set; }

		[JsonPropertyName("cTag")]
		public string CTag { get; set; }

		[JsonPropertyName("size")]
		public long Size { get; set; }

		[JsonPropertyName("createdBy")]
		public CreatedBy CreatedBy { get; set; }

		[JsonPropertyName("lastModifiedBy")]
		public LastModifiedBy LastModifiedBy { get; set; }

		[JsonPropertyName("parentReference")]
		public ParentReference ParentReference { get; set; }

		[JsonPropertyName("file")]
		public FileMeta File { get; set; }

		[JsonPropertyName("fileSystemInfo")]
		public FileSystemInfo FileSystemInfo { get; set; }
	}
}
