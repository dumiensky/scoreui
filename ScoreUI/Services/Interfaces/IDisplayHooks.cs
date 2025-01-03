using ScoreUI.Models.Entities;
using ScoreUI.Models.Events;

namespace ScoreUI.Services.Interfaces;

public interface IDisplayHooks
{
	event EventHandler<CurrentMatchChangedEventArgs>? CurrentMatchChanged;
	void SetCurrentMatch(Guid tournamentId, Match? match);
}