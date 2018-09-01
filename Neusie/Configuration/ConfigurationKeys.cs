namespace Neusie.Configuration
{
	internal static class ConfigurationKeys
	{
		internal const string InputSection = "input";

		internal static class Input
		{
			internal const string Sources = "sources";
			internal const string Blacklist = "blacklist";
			internal const string BlacklistFile = "blacklistfile";
			internal const string MinWordLength = "minwordlength";
			internal const string KeepComments = "keepcomments";
			internal const string KeepStrings = "keepstrings";
			internal const string KeepNamespaces = "keepnamespaces";
		}
	}
}