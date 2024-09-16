using System.Text.Json.Serialization;

namespace O365Poc.Server.Models
{
	public class OneDriveFilesRS
	{
		[JsonPropertyName("@odata.context")]
		public string ODataContext { get; set; }

		[JsonPropertyName("value")]
		public List<OneDriveItem> Items { get; set; }
	}

	public class OneDriveItem
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("eTag")]
		public string ETag { get; set; }

		[JsonPropertyName("cTag")]
		public string CTag { get; set; }

		[JsonPropertyName("createdDateTime")]
		public DateTime CreatedDateTime { get; set; }

		[JsonPropertyName("lastModifiedDateTime")]
		public DateTime LastModifiedDateTime { get; set; }

		[JsonPropertyName("size")]
		public long Size { get; set; }

		[JsonPropertyName("webUrl")]
		public string WebUrl { get; set; }

		[JsonPropertyName("createdBy")]
		public CreatedBy CreatedBy { get; set; }

		[JsonPropertyName("lastModifiedBy")]
		public LastModifiedBy LastModifiedBy { get; set; }

		[JsonPropertyName("parentReference")]
		public ParentReference ParentReference { get; set; }

		[JsonPropertyName("fileSystemInfo")]
		public FileSystemInfo FileSystemInfo { get; set; }

		[JsonPropertyName("folder")]
		public Folder Folder { get; set; }

		[JsonPropertyName("file")]
		public FileMeta File { get; set; }

		[JsonPropertyName("shared")]
		public Shared Shared { get; set; }
	}

	public class CreatedBy
	{
		[JsonPropertyName("application")]
		public ApplicationInfo Application { get; set; }

		[JsonPropertyName("user")]
		public UserInfo User { get; set; }
	}

	public class LastModifiedBy
	{
		[JsonPropertyName("application")]
		public ApplicationInfo Application { get; set; }

		[JsonPropertyName("user")]
		public UserInfo User { get; set; }
	}

	public class ApplicationInfo
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("displayName")]
		public string DisplayName { get; set; }
	}

	public class UserInfo
	{
		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("displayName")]
		public string DisplayName { get; set; }
	}

	public class ParentReference
	{
		[JsonPropertyName("driveType")]
		public string DriveType { get; set; }

		[JsonPropertyName("driveId")]
		public string DriveId { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("path")]
		public string Path { get; set; }

		[JsonPropertyName("siteId")]
		public string SiteId { get; set; }
	}

	public class FileSystemInfo
	{
		[JsonPropertyName("createdDateTime")]
		public DateTime CreatedDateTime { get; set; }

		[JsonPropertyName("lastModifiedDateTime")]
		public DateTime LastModifiedDateTime { get; set; }
	}

	public class Folder
	{
		[JsonPropertyName("childCount")]
		public int ChildCount { get; set; }
	}

	public class FileMeta
	{
		[JsonPropertyName("mimeType")]
		public string MimeType { get; set; }

		[JsonPropertyName("hashes")]
		public Hashes Hashes { get; set; }
	}

	public class Hashes
	{
		[JsonPropertyName("quickXorHash")]
		public string QuickXorHash { get; set; }
	}

	public class Shared
	{
		[JsonPropertyName("scope")]
		public string Scope { get; set; }
	}

}
