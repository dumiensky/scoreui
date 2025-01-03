using System.Drawing;

namespace ScoreUI.Models.Helpers;

public static class ColorUtilities
{
	public static string GetLuminanceContrastColor(string colorHex)
	{
		var color = ColorTranslator.FromHtml(colorHex);
		
		var luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

		return luminance > 0.4 ? "#000000" : "#FFFFFF";
	}
}