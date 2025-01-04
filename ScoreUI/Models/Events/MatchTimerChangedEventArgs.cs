using ScoreUI.Models.Entities;

namespace ScoreUI.Models.Events;

public class MatchTimerChangedEventArgs : EventArgs
{
	public Guid MatchId { get; }
	
	public TimeSpan? CurrentTime { get; }

	public MatchTimerChangedEventArgs(Guid matchId, TimeSpan? currentTime)
	{
		MatchId = matchId;
		CurrentTime = currentTime;
	}
}