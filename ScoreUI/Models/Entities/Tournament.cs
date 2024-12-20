using MongoDB.Wrapper;

namespace ScoreUI.Models.Entities;

public class Tournament : Entity
{
	public required string Name { get; set; }
	
	public string? ShortName { get; set; }

	public List<Participant> Participants { get; set; } = new();

	public List<Match> Matches { get; set; } = new();

	public TournamentSettings Settings { get; set; } = new();
}

public class TournamentSettings
{
	public List<Type> AllowedMatchTypes { get; set; } = new();

	public SimpleMatchSettings SimpleMatch { get; set; } = new();
}

public abstract class MatchSettings
{
	public bool IsTimed { get; set; }

	public TimeSpan? MaxTime { get; set; }
	
	public bool OvertimeAllowed { get; set; }
	
	public bool CountTimerDown { get; set; }
}

public class SimpleMatchSettings : MatchSettings
{
	
}

public class DualPointsMatchSettings : MatchSettings
{
	public string? ScoreAName { get; set; }
	public string? ScoreBName { get; set; }
}

public class MultiDuelMatchSettings : MatchSettings
{
	public int MinDuels { get; set; } = 1;
	public int MaxDuels { get; set; } = 5;
	public bool ResetTimeOnNewMatch { get; set; }
}