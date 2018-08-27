using System.Collections.Generic;

namespace Neusie.Parsing
{
	internal interface ISourceParser
	{
		IEnumerable<string> Files( string root );
	}
}