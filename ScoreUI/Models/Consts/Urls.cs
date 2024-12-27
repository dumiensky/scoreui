namespace ScoreUI.Models.Consts;

public static class Urls
{
	public const string Home = "/";
	public const string Error = "/error";

	public static class Tournaments
	{
		const string Index = "/tournaments";
		public const string Create = Index + "/create";
		public const string ManageId = Index + "/{TournamentId:guid}";
		public const string ManageAlias = Index + "/{TournamentAlias}";

		public static string GetManageId(Guid tournamentId) => $"{Index}/{tournamentId}";
		public static string GetManageAlias(string alias) => $"{Index}/{alias}";
	}
}