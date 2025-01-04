using Microsoft.AspNetCore.Components;
using ScoreUI.Models.Entities;
using ScoreUI.Models.Enums;
using ScoreUI.Models.Events;
using ScoreUI.Models.Helpers;
using ScoreUI.Services.Interfaces;

namespace ScoreUI.Components.Displays;

public abstract class DisplayBase : ComponentBase, IDisposable
{
	[Inject]
	public required IDisplayHooks DisplayHooks { get; set; }
	
	[Parameter]
	public required Tournament Tournament { get; set; }

	protected Match? CurrentMatch;
	protected Participant? ParticipantOne;
	protected Participant? ParticipantTwo;

	protected override void OnInitialized()
	{
		DisplayHooks.CurrentMatchChanged += DisplayHooksOnCurrentMatchChanged;
		DisplayHooks.TournamentUpdated += DisplayHooksOnTournamentUpdated;
	}

	void DisplayHooksOnTournamentUpdated(object? sender, Tournament e)
	{
		if (e.Id == Tournament.Id)
		{
			Tournament = e;
			InvokeAsync(StateHasChanged);
		}
	}

	void DisplayHooksOnCurrentMatchChanged(object? sender, CurrentMatchChangedEventArgs e)
	{
		if (e.TournamentId == Tournament.Id)
		{
			CurrentMatch = e.CurrentMatch;

			if (CurrentMatch is not null)
			{
				ParticipantOne = Tournament.GetParticipant(CurrentMatch.OneId);
				ParticipantTwo = Tournament.GetParticipant(CurrentMatch.TwoId);
			}
			else
			{
				ParticipantOne = null;
				ParticipantTwo = null;
			}
			
			InvokeAsync(StateHasChanged);
		}
	}

	public void Dispose()
	{
		DisplayHooks.CurrentMatchChanged -= DisplayHooksOnCurrentMatchChanged;
	}

	protected string GetTournamentName() =>
		Tournament.Settings.Displays.UseShortTournamentName && !string.IsNullOrWhiteSpace(Tournament.ShortName)
			? Tournament.ShortName
			: Tournament.Name;

	protected string GetParticipantName(Participant? participant)
	{
		if (participant is null)
			return string.Empty;

		return Tournament.Settings.Displays.UseShortNames
			? participant.GetShortName()
			: participant.Name;
	}
	
	protected string GetBackgroundColor(Participant? participant) => Tournament.Settings.Displays.ColorMode switch
	{
		DisplaysColorMode.Text when participant?.ColorHex is not null =>
			ColorUtilities.GetLuminanceContrastColor(participant.ColorHex),
		DisplaysColorMode.Background when participant?.ColorHex is not null => participant.ColorHex,
		_ => "#fff"
	};

	protected string GetTextColorStyle(Participant? participant) => Tournament.Settings.Displays.ColorMode switch
	{
		DisplaysColorMode.Text when participant?.ColorHex is not null => $"color: {participant.ColorHex}",
		DisplaysColorMode.Background when participant?.ColorHex is not null =>
			$"color: {ColorUtilities.GetLuminanceContrastColor(participant.ColorHex)}",
		_ => "color: #000"
	};
}