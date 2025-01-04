using ScoreUI.Models.Entities;

namespace ScoreUI.Models.Events;

public class CurrentDuelChangedEventArgs : EventArgs
{
	public Guid MatchId { get; }
	
	public MultiDuelMatch.Duel? CurrentDuel { get; }

	public CurrentDuelChangedEventArgs(Guid matchId, MultiDuelMatch.Duel? currentDuel)
	{
		MatchId = matchId;
		CurrentDuel = currentDuel;
	}
}