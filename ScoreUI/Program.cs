using MongoDB.Wrapper;
using MongoDB.Wrapper.Abstractions;
using MongoDB.Wrapper.Settings;
using MudBlazor.Services;
using ScoreUI.Components;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = new MongoDbSettings();
builder.Configuration.GetSection(nameof(mongoDbSettings)).Bind(mongoDbSettings);

builder
	.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddSingleton<IMongoDb>(new MongoDb(mongoDbSettings));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app
	.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();