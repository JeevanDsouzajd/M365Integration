namespace O365Poc.Server.Models
{
	public class AzureAdConfig
	{
		public string TenantId { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string RedirectUri { get; set; }
		public string Scope { get; set; }

	}
}
