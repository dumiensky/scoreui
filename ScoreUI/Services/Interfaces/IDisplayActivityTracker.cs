namespace ScoreUI.Services.Interfaces;

public interface IDisplayActivityTracker
{
	void SetActive(Guid displayId);
	void RemoveActive(Guid displayId);
	int GetActiveAmount(Guid displayId);
	event EventHandler<Guid>? DisplayActivityChanged;
}