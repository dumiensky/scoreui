using MudBlazor;

namespace ScoreUI.Models.Consts;

public static class Theme
{
	public static MudTheme Default = new()
	{
		Typography = new()
		{
			H1 = new()
			{
				FontSize = "4rem",
				FontWeight = 500,
				LineHeight = 1.167,
				LetterSpacing = "0"
			},
			H2 = new()
			{
				FontSize = "3rem",
				FontWeight = 500,
				LineHeight = 1.167,
				LetterSpacing = "0"
			},
			Subtitle1 = new()
			{
				FontSize = "1rem",
				FontWeight = 500,
				LineHeight = 1.75,
				LetterSpacing = ".00938em"
			}
		}
	};
}