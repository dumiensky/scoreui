using ScoreUI.Models.Entities;
using ScoreUI.Models.Events;
using ScoreUI.Services.Interfaces;

namespace ScoreUI.Services;

public class DisplayHooks : IDisplayHooks
{
	public event EventHandler<CurrentMatchChangedEventArgs>? CurrentMatchChanged;
	public event EventHandler<CurrentDuelChangedEventArgs>? CurrentDuelChanged;
	public event EventHandler<MatchTimerChangedEventArgs>? MatchTimerChanged; 
	public event EventHandler<Tournament>? TournamentUpdated; 

	public void SetCurrentMatch(Guid tournamentId, Match? match)
	{
		CurrentMatchChanged?.Invoke(this, new(tournamentId, match));
	}

	public void SetCurrentDuel(Guid matchId, MultiDuelMatch.Duel? duel)
	{
		CurrentDuelChanged?.Invoke(this, new(matchId, duel));
	}

	public void SendTournamentUpdated(Tournament tournament)
	{
		TournamentUpdated?.Invoke(this, tournament);
	}

	public void SendTimerChanged(Guid matchId, TimeSpan? timer)
	{
		MatchTimerChanged?.Invoke(this, new(matchId, timer));
	}
}