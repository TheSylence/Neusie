using System.Collections.Generic;
using System.Linq;

namespace Neusie.TextProcessing
{
	internal class ShortWordRemover : ITextPostProcessor
	{
		public ShortWordRemover( int threshold )
		{
			Threshold = threshold;
		}

		/// <inheritdoc />
		public Dictionary<string, int> Process( Dictionary<string, int> result )
		{
			var keysToRemove = result.Keys.Where( k => k.Length < Threshold ).ToList();

			foreach( var key in keysToRemove )
			{
				result.Remove( key );
			}

			return result;
		}

		private readonly int Threshold;
	}
}