using MongoDB.Wrapper;
using ScoreUI.Models.Enums;

namespace ScoreUI.Models.Entities;

public class Display : Entity
{
	public DisplayType Type { get; set; }
	
	public Guid TournamentId { get; set; }

	public static Display Create(DisplayType type, Guid tournamentId) =>
		new()
		{
			Id = Guid.NewGuid(),
			Added = DateTimeOffset.Now,
			Type = type,
			TournamentId = tournamentId
		};
}