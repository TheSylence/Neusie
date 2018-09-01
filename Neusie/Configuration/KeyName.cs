namespace Neusie.Configuration
{
	internal static class KeyName
	{
		internal static string SuffixWithCounter( string key, int counter )
		{
			return $"{key}:{counter}";
		}
	}
}