using System.Collections.Generic;

namespace Neusie.Generation.Image
{
	internal interface IWordPlacer
	{
		IEnumerable<WordPlacement> Place( IEnumerable<KeyValuePair<string, int>> words );
	}
}