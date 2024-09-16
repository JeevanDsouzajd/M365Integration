using O365Poc.Server.Models;
using O365Poc.Server.Services;

namespace O365Poc.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			builder.Services.Configure<AzureAdConfig>(builder.Configuration.GetSection("AzureAD"));

			// Add services to the container.
			builder.Services.AddSingleton<AccountService>();
			builder.Services.AddScoped<OneDriveService>();
			builder.Services.AddSingleton<CalendarService>();

			builder.Services.AddHttpClient();
			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseDefaultFiles();
			app.UseStaticFiles();

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.MapFallbackToFile("/index.html");

			app.Run();
		}
	}
}
