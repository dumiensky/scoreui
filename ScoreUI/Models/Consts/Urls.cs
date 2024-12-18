namespace ScoreUI.Models.Consts;

public static class Urls
{
	public const string Home = "/";
	public const string Error = "/error";

	public static class Tournaments
	{
		const string Index = "/tournaments";
		public const string Create = Index + "/create";
		public const string Details = Index + "/{TournamentId:guid}";
	}
}