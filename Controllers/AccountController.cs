using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O365Poc.Server.Models;
using O365Poc.Server.Services;

namespace O365Poc.Server.Controllers
{
	[Route("Account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly AccountService _accountService;

		public AccountController(AccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpGet("")]
		public string Index()
		{
			return "Microsoft Account Management";
		}

		[HttpGet("UserConsent")]
		public string GetUserConsent()
		{
			var consent = _accountService.GetUserConsent();
			return consent;
		}

		[HttpPost("Callback")]
		public async Task<TokenResponse> Callback([FromForm] string code)
		{
			var result = await _accountService.Callback(code);
			return result;
		}

		[HttpGet("GetToken")]
		public async Task<TokenResponse> GetToken()
		{
			var result = await _accountService.GetTokenFromFile();
			return result;
		}

		[HttpPost("NewAccessToken")]
		public async Task<ActionResult<string>> GetNewAccessToken()
		{
			var refresh_token = await _accountService.GetNewAccessToken();
			return refresh_token;
		}
	}
}
