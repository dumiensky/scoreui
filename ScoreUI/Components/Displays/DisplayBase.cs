using Microsoft.AspNetCore.Components;
using ScoreUI.Models.Entities;
using ScoreUI.Models.Events;
using ScoreUI.Services.Interfaces;

namespace ScoreUI.Components.Displays;

public abstract class DisplayBase : ComponentBase, IDisposable
{
	[Inject]
	public required IDisplayHooks DisplayHooks { get; set; }
	
	[Parameter]
	public required Tournament Tournament { get; set; }

	protected Match? CurrentMatch;

	protected override void OnInitialized()
	{
		DisplayHooks.CurrentMatchChanged += DisplayHooksCurrentMatchChanged;
	}

	void DisplayHooksCurrentMatchChanged(object? sender, CurrentMatchChangedEventArgs e)
	{
		if (e.TournamentId == Tournament.Id)
		{
			CurrentMatch = e.CurrentMatch;
			InvokeAsync(StateHasChanged);
		}
	}

	public void Dispose()
	{
		DisplayHooks.CurrentMatchChanged -= DisplayHooksCurrentMatchChanged;
	}
}