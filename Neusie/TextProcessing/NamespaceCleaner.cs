using System;
using System.Linq;

namespace Neusie.TextProcessing
{
	internal class NamespaceCleaner : ITextPreProcessor
	{
		/// <inheritdoc />
		public string Process( string input )
		{
			var lines = input.Split( Environment.NewLine ).AsEnumerable();

			lines = lines.Where( l => !l.StartsWith( "namespace ", StringComparison.Ordinal ) );
			lines = lines.Where( l => !l.StartsWith( "using ", StringComparison.Ordinal ) );

			return string.Join( Environment.NewLine, lines );
		}
	}
}