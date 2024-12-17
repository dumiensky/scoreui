using MongoDB.Wrapper;

namespace ScoreUI.Models.Entities;

public class Participant : Entity
{
	public required string Name { get; set; }
	
	public string? ShortName { get; set; }
	
	public string? ImageBase64 { get; set; }
	
	public string? ColorHex { get; set; }
}