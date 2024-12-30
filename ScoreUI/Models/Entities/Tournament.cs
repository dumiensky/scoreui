using System.Security.Cryptography;
using System.Text;
using MongoDB.Wrapper;

namespace ScoreUI.Models.Entities;

public class Tournament : Entity
{
	public required string Name { get; set; }
	
	public string? ShortName { get; set; }

	public List<Participant> Participants { get; set; } = new();

	public List<Match> Matches { get; set; } = new();

	public TournamentSettings Settings { get; set; } = new();

	public IEnumerable<Participant> ActiveParticipants => Participants.Where(_ => !_.Deleted);

	public IEnumerable<Match> ActiveMatches => Matches.Where(_ => !_.Deleted);

	public Participant? GetParticipant(Guid participantId) =>
		Participants.FirstOrDefault(_ => _.Id == participantId);
	
	public string GetTournamentToken() =>
		Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes($"v?{Settings.Key}8{Id}23")));
}

public class TournamentSettings
{
	public SimpleMatchSettings SimpleMatch { get; set; } = new();

	public DualPointsMatchSettings DualPointsMatch { get; set; } = new();

	public MultiDuelMatchSettings MultiDuelMatch { get; set; } = new();
	
	public string? Alias { get; set; }
	
	public string? Key { get; set; }
}

public abstract class MatchSettings
{
	public bool IsEnabled { get; set; }
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