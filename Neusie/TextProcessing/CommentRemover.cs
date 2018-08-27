using System.Text.RegularExpressions;

namespace Neusie.TextProcessing
{
	internal class CommentRemover : ITextPreProcessor
	{
		static CommentRemover()
		{
			BlockPattern = new Regex( @"(/\*(.*?)\*/)", RegexOptions.Compiled | RegexOptions.Singleline );
			LinePattern = new Regex( "(//(.*)\r?\n?)", RegexOptions.Compiled | RegexOptions.Multiline );
		}

		/// <inheritdoc />
		public string Process( string input )
		{
			input = LinePattern.Replace( input, string.Empty );
			input = BlockPattern.Replace( input, string.Empty );

			return input;
		}

		private static readonly Regex BlockPattern;

		private static readonly Regex LinePattern;
	}
}