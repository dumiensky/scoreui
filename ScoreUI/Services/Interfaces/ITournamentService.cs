using ScoreUI.Models.Entities;

namespace ScoreUI.Services.Interfaces;

public interface ITournamentService
{
	Task Create(Tournament tournament);
	Task<Tournament?> Get(Guid tournamentId);
}