namespace Neusie.Configuration
{
	internal static class ConfigurationKeys
	{
		internal const string CsvOutputSection = "csv";
		internal const string ImageOutputSection = "image";
		internal const string InputSection = "input";
		internal const string OutputSection = "output";

		internal static class CsvOutput
		{
			internal const string Enabled = "enable";
		}

		internal static class ImageOutput
		{
			internal const string Compactness = "compactness";
			internal const string Enabled = "enable";
			internal const string Font = "font";
			internal const string Height = "height";
			internal const string MinimumFontSize = "minimumfontsize";
			internal const string Width = "width";
		}

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
			internal const string Seed = "seed";
			internal const string TargetName = "targetname";
			internal const string TargetPath = "targetpath";
		}
	}
}