using System.ComponentModel.DataAnnotations;

namespace ScoreUI.Models.Enums;

public enum DisplayType
{
	[Display(Name = "Nakładka na stream")]
	StreamOverlay,
	[Display(Name = "Ekran wolnostojący")]
	Standalone
}