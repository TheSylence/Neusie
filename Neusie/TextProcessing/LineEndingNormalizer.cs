namespace Neusie.TextProcessing
{
	internal class LineEndingNormalizer : ITextPreProcessor
	{
		/// <inheritdoc />
		public string Process( string input )
		{
			input = input.Replace( "\r\n", "\r" );
			input = input.Replace( "\r", "\n" );
			input = input.Replace( "\n", "\r\n" );

			return input;
		}
	}
}