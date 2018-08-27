namespace Neusie.TextProcessing
{
	internal class SymbolExpander : ITextPreProcessor
	{
		static SymbolExpander()
		{
			Symbols = new[]
			{
				'<', '>', '{', '}', '(', ')', '/', '\\', '.', ',', ';', '\"', '\'', ']',
				'[', '#', '=', '_', '-', ':', '!', '?'
			};
		}

		/// <inheritdoc />
		public string Process( string input )
		{
			foreach( var c in Symbols )
			{
				input = input.Replace( c.ToString(), $" {c} " );
			}

			return input;
		}

		private static readonly char[] Symbols;
	}
}