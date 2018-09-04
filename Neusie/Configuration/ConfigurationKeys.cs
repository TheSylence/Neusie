namespace Neusie.Configuration
{
	internal static class ConfigurationKeys
	{
		internal const string InputSection = "input";
		internal const string OutputSection = "output";

		internal static class Input
		{
			internal const string Blacklist = "blacklist";
			internal const string BlacklistFile = "blacklistfile";
			internal const string KeepComments = "keepcomments";
			internal const string KeepNamespaces = "keepnamespaces";
			internal const string KeepStrings = "keepstrings";
			internal const string MinWordLength = "minwordlength";
			internal const string Sources = "sources";
		}

		internal static class Output
		{
			internal const string TargetPath = "targetpath";
		}
	}
}