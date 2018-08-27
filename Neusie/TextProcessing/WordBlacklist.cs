using System.Collections.Generic;
using System.Linq;

namespace Neusie.TextProcessing
{
	internal class WordBlacklist : ITextPostProcessor
	{
		public WordBlacklist( IEnumerable<string> blackList )
		{
			BlackList = blackList.ToList();
		}

		/// <inheritdoc />
		public Dictionary<string, int> Process( Dictionary<string, int> result )
		{
			foreach( var word in BlackList )
			{
				result.Remove( word );
			}

			return result;
		}

		private readonly List<string> BlackList;
	}
}