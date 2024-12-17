namespace ScoreUI.Models.Entities;

public abstract class Match
{
	public Guid OneId { get; set; }
	public Guid TwoId { get; set; }

	public abstract Guid? GetWinnerId();
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

	public class Duel
	{
		public int ScoreOne { get; set; }
		public int ScoreTwo { get; set; }
		
		public Guid? GetWinnerId(Guid oneId, Guid twoId) => 
			ScoreOne.CompareTo(ScoreTwo) switch
			{
				> 0 => oneId,
				< 0 => twoId,
				_ => null
			};
	}
}