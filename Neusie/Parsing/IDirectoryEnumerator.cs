using System.Collections.Generic;

namespace Neusie.Parsing
{
	internal interface IDirectoryEnumerator
	{
		IEnumerable<string> Files( string root, string pattern, bool recursive );
	}
}