using ScoreUI.Models.Entities;

namespace ScoreUI.Models.Events;

public class CurrentMatchChangedEventArgs : EventArgs
{
	public Guid TournamentId { get; }
	
	public Match? CurrentMatch { get; }

	public CurrentMatchChangedEventArgs(Guid tournamentId, Match? currentMatch)
	{
		TournamentId = tournamentId;
		CurrentMatch = currentMatch;
	}
}