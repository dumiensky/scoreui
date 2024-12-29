using MongoDB.Wrapper;

namespace ScoreUI.Models.Entities;

public class Participant : Entity
{
	public required string Name { get; set; }
	
	public string? ShortName { get; set; }
	
	public string? ImageBase64 { get; set; }
	
	public string? ColorHex { get; set; }

	public string? GetShortName() => ShortName ?? Name;

	public static Participant Create(string name, string? shortName, string? imageBase64, string? colorHex) =>
		new()
		{
			Id = Guid.NewGuid(),
			Added = DateTimeOffset.Now,
			Name = name,
			ShortName = shortName,
			ImageBase64 = imageBase64,
			ColorHex = colorHex
		};
}