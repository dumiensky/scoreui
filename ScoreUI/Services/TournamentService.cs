using MongoDB.Wrapper.Abstractions;
using ScoreUI.Models.Entities;
using ScoreUI.Services.Interfaces;

namespace ScoreUI.Services;

public class TournamentService(IMongoDb mongoDb) : ITournamentService
{
	public async Task Create(Tournament tournament)
	{
		await mongoDb.Add(tournament);
	}

	public async Task<Tournament?> Get(Guid tournamentId) =>
		await mongoDb.Get<Tournament>(tournamentId);
}