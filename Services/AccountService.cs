using Microsoft.Extensions.Options;
using O365Poc.Server.Models;
using static System.Formats.Asn1.AsnWriter;
using System.Text.Json;
using System.Xml.Linq;

namespace O365Poc.Server.Services
{
	public class AccountService
	{
		private readonly AzureAdConfig _azureAdConfig;

		public AccountService(IOptions<AzureAdConfig> azureAdOptions, IConfiguration config)
		{
			_azureAdConfig = azureAdOptions.Value;
		}

		string tokenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "AzureToken.json");

		public string GetUserConsent()
		{
			var authorizationUrl = $"https://login.microsoftonline.com/common/oauth2/v2.0/authorize" +
			 	$"?client_id={_azureAdConfig.ClientId}" +
				$"&response_type=code" +
				$"&redirect_uri={Uri.EscapeDataString(_azureAdConfig.RedirectUri)}" +
				$"&response_mode=form_post" +
				$"&scope={Uri.EscapeDataString(_azureAdConfig.Scope)}" +
				$"&state= authorize" +
				$"&prompt=select_account";
			return authorizationUrl;
		}

		public async Task<TokenResponse> Callback(string code)
		{
			var tokenUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
			var client = new HttpClient();

			var tokenRequestParameters = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("client_id", _azureAdConfig.ClientId),
				new KeyValuePair<string, string>("client_secret", _azureAdConfig.ClientSecret),
				new KeyValuePair<string, string>("code", code),
				new KeyValuePair<string, string>("redirect_uri", _azureAdConfig.RedirectUri),
				new KeyValuePair<string, string>("grant_type", "authorization_code"),
				new KeyValuePair<string, string>("scope", _azureAdConfig.Scope)
			};

			var request = new FormUrlEncodedContent(tokenRequestParameters);
			var response = await client.PostAsync(tokenUrl, request);

			string content = await response.Content.ReadAsStringAsync();
			var JsonResponse = JsonSerializer.Deserialize<TokenResponse>(content);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				File.WriteAllText(tokenFilePath, content);
			}

			return JsonResponse;
		}
			
		public async Task<TokenResponse> GetTokenFromFile()
		{
			var token = await File.ReadAllTextAsync(tokenFilePath);
			var JsonResponse = JsonSerializer.Deserialize<TokenResponse>(token);
			return JsonResponse;
		}

		public async Task<string> GetNewAccessToken()
		{
			var tokenUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
			string fileContent = File.ReadAllText(tokenFilePath);
			TokenResponse data = JsonSerializer.Deserialize<TokenResponse>(fileContent);
			var client = new HttpClient();

			var tokenRequestParameters = new List<KeyValuePair<string, string>>
			{
			new KeyValuePair<string, string>("client_id", _azureAdConfig.ClientId),
			new KeyValuePair<string, string>("client_secret", _azureAdConfig.ClientSecret),
			new KeyValuePair<string, string>("refresh_token", data.refresh_token.ToString()),
			new KeyValuePair<string, string>("redirect_uri", _azureAdConfig.RedirectUri),
			new KeyValuePair<string, string>("grant_type", "refresh_token"),
			new KeyValuePair<string, string>("scope", _azureAdConfig.Scope)
			};

			var request = new FormUrlEncodedContent(tokenRequestParameters);
			var response = await client.PostAsync(tokenUrl, request);

			string content = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				File.WriteAllText(tokenFilePath, content);
			}

			return content;
		}
	}
}
