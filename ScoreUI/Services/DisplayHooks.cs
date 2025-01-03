using ScoreUI.Models.Entities;
using ScoreUI.Models.Events;
using ScoreUI.Services.Interfaces;

namespace ScoreUI.Services;

public class DisplayHooks : IDisplayHooks
{
	public event EventHandler<CurrentMatchChangedEventArgs>? CurrentMatchChanged;
	public event EventHandler<Tournament>? TournamentUpdated; 

	public void SetCurrentMatch(Guid tournamentId, Match? match)
	{
		CurrentMatchChanged?.Invoke(this, new(tournamentId, match));
	}

	public void SendTournamentUpdated(Tournament tournament)
	{
		TournamentUpdated?.Invoke(this, tournament);
	}
}