using System.Text.RegularExpressions;

namespace Neusie.TextProcessing
{
	internal class StringRemover : ITextPreProcessor
	{
		static StringRemover()
		{
			var strings = @"""((\\[^\n]|[^""\n])*)""";
			var verbatimStrings = @"@(""[^""]*"")+";

			var pattern = $"{strings}|{verbatimStrings}";
			Pattern = new Regex( pattern, RegexOptions.Multiline | RegexOptions.Compiled );
		}

		/// <inheritdoc />
		public string Process( string input )
		{
			return Pattern.Replace( input, string.Empty );
		}

		private static readonly Regex Pattern;
	}
}