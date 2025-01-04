using ScoreUI.Services.Interfaces;

namespace ScoreUI.Services;

public class DisplayActivityTracker : IDisplayActivityTracker
{
	public event EventHandler<Guid>? DisplayActivityChanged; 
	
	readonly Dictionary<Guid, int> _activeConnections = new();

	public void SetActive(Guid displayId)
	{
		_activeConnections.TryAdd(displayId, 0);
		_activeConnections[displayId] += 1;
		
		DisplayActivityChanged?.Invoke(this, displayId);
	}

	public void RemoveActive(Guid displayId)
	{
		if (_activeConnections.ContainsKey(displayId))
		{
			_activeConnections[displayId] -= 1;
			
			DisplayActivityChanged?.Invoke(this, displayId);
		}
	}

	public int GetActiveAmount(Guid displayId)
	{
		return _activeConnections.GetValueOrDefault(displayId, 0);
	}
}