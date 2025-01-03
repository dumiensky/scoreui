using MongoDB.Wrapper.Abstractions;
using ScoreUI.Models.Entities;
using ScoreUI.Services.Interfaces;

namespace ScoreUI.Services;

public class TournamentService(IMongoDb mongoDb, IDisplayHooks displayHooks) : ITournamentService
{
	public async Task Create(Tournament tournament)
	{
		await mongoDb.Add(tournament);
	}

	public async Task<Tournament?> Get(Guid tournamentId) =>
		await mongoDb.Get<Tournament>(tournamentId);

	public async Task<Tournament?> FindByAlias(string alias) =>
		await mongoDb.FirstOrDefault<Tournament>(_ => _.Settings.Alias == alias.ToLower());

	public async Task<bool> IsAliasFree(string? alias)
	{
		if (alias is null)
			return true;

		return !await mongoDb.Any<Tournament>(_ => _.Settings.Alias!.ToLower() == alias.ToLower());
	}

	public async Task<bool> Update(Tournament tournament)
	{
		var result = await mongoDb.Replace(tournament);

		if (result)
			displayHooks.SendTournamentUpdated(tournament);
        
		return result;
	}
}