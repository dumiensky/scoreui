using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Wrapper;
using ScoreUI.Models.Enums;

namespace ScoreUI.Models.Entities;

[BsonKnownTypes(
	typeof(SimpleMatch), 
	typeof(DualPointsMatch), 
	typeof(MultiDuelMatch))]
public abstract class Match : Entity
{
	public MatchStatus Status { get; set; }
	public Guid OneId { get; set; }
	public Guid TwoId { get; set; }
	public string? DisplayText { get; set; }
	public string? Comment { get; set; }

	public abstract Guid? GetWinnerId();
	public abstract MatchSettings GetSettings(Tournament tournament);

	public string GetMatchToken(string? key) =>
		Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes($"{key}x{Id}7")));
	
	public bool ScoringDisabled => Status is MatchStatus.Done;
	public bool ScoringEnabled => Status is MatchStatus.Pending;
}

public class SimpleMatch : Match
{
	public int ScoreOne { get; set; }
	public int ScoreTwo { get; set; }

	public override Guid? GetWinnerId() => 
		ScoreOne.CompareTo(ScoreTwo) switch
		{
			> 0 => OneId,
			< 0 => TwoId,
			_ => null
		};

	public override MatchSettings GetSettings(Tournament tournament) =>
		tournament.Settings.SimpleMatch;

	public static SimpleMatch Create(Guid oneId, Guid twoId, string? displayText, string? comment) =>
		new()
		{
			Added = DateTimeOffset.Now,
			Id = Guid.NewGuid(),
			OneId = oneId,
			TwoId = twoId,
			DisplayText = displayText,
			Comment = comment
		};
}

public class DualPointsMatch : Match
{
	public int ScoreOneA { get; set; }
	public int ScoreTwoA { get; set; }
	public int ScoreOneB { get; set; }
	public int ScoreTwoB { get; set; }
	
	public override Guid? GetWinnerId() =>
		ScoreOneA.CompareTo(ScoreTwoA) switch
		{
			> 0 => OneId,
			< 0 => TwoId,
			_ => ScoreOneB.CompareTo(ScoreTwoB) switch
			{
				> 0 => OneId,
				< 0 => TwoId,
				_ => null
			}
		};

	public override MatchSettings GetSettings(Tournament tournament) =>
		tournament.Settings.DualPointsMatch;

	public static DualPointsMatch Create(Guid oneId, Guid twoId, string? displayText, string? comment) =>
		new()
		{
			Added = DateTimeOffset.Now,
			Id = Guid.NewGuid(),
			OneId = oneId,
			TwoId = twoId,
			DisplayText = displayText,
			Comment = comment
		};
}

public class MultiDuelMatch : Match
{
	public List<Duel> Duels { get; set; } = new();
	
	public override Guid? GetWinnerId()
	{
		var results = Duels.Select(_ => _.GetWinnerId(OneId, TwoId));
		var winsOne = results.Count(_ => _ == OneId);
		var winsTwo = results.Count(_ => _ == TwoId);
		
		return winsOne.CompareTo(winsTwo) switch
		{
			> 0 => OneId,
			< 0 => TwoId,
			_ => null
		};
	}

	public override MatchSettings GetSettings(Tournament tournament) =>
		tournament.Settings.MultiDuelMatch;

	public class Duel
	{
		public Guid Id { get; set; }
		public int ScoreOne { get; set; }
		public int ScoreTwo { get; set; }
		
		public Guid? GetWinnerId(Guid oneId, Guid twoId) => 
			ScoreOne.CompareTo(ScoreTwo) switch
			{
				> 0 => oneId,
				< 0 => twoId,
				_ => null
			};

		public static Duel Create() => new()
		{
			Id = Guid.NewGuid()
		};
	}
	
	public static MultiDuelMatch Create(Guid oneId, Guid twoId, string? displayText, string? comment) =>
		new()
		{
			Added = DateTimeOffset.Now,
			Id = Guid.NewGuid(),
			OneId = oneId,
			TwoId = twoId,
			DisplayText = displayText,
			Comment = comment
		};
}