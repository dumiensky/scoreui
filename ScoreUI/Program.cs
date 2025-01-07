using ClipLazor.Extention;
using MongoDB.Wrapper;
using MongoDB.Wrapper.Abstractions;
using MongoDB.Wrapper.Settings;
using MudBlazor.Services;
using ScoreUI.Pages;
using ScoreUI.Services;
using ScoreUI.Services.Interfaces;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = new MongoDbSettings();
builder.Configuration.GetSection(nameof(mongoDbSettings)).Bind(mongoDbSettings);

builder
	.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddSerilog(
	c =>
	{
		c.WriteTo.Console();
		c.WriteTo.MongoDB($"{mongoDbSettings.ConnectionString}/{mongoDbSettings.DatabaseName}");
		
		c.Enrich.FromLogContext();
		c.Enrich.WithClientIp();
		
		c.MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning);
		c.MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning);
		c.MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning);
		c.MinimumLevel.Override("Serilog.AspNetCore.RequestLoggingMiddleware", LogEventLevel.Warning);
	});
builder.Services.AddMudServices();
builder.Services.AddClipboard();
builder.Services.AddSingleton<IMongoDb>(new MongoDb(mongoDbSettings));
builder.Services.AddSingleton<IDisplayHooks, DisplayHooks>();
builder.Services.AddSingleton<IDisplayActivityTracker, DisplayActivityTracker>();
builder.Services.AddTransient<ITournamentService, TournamentService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseAntiforgery();

app
	.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();