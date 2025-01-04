using ScoreUI.Models.Entities;
using ScoreUI.Models.Events;

namespace ScoreUI.Services.Interfaces;

public interface IDisplayHooks
{
	event EventHandler<CurrentMatchChangedEventArgs>? CurrentMatchChanged;
	event EventHandler<CurrentDuelChangedEventArgs>? CurrentDuelChanged;
	event EventHandler<Tournament>? TournamentUpdated;
	void SetCurrentMatch(Guid tournamentId, Match? match);
	void SetCurrentDuel(Guid matchId, MultiDuelMatch.Duel? duel);
	void SendTournamentUpdated(Tournament tournament);
}