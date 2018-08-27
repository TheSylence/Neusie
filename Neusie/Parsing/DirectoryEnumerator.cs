using System.Collections.Generic;
using System.IO;

namespace Neusie.Parsing
{
	internal class DirectoryEnumerator : IDirectoryEnumerator
	{
		/// <inheritdoc />
		public IEnumerable<string> Files( string root, string pattern, bool recursive )
		{
			return Directory.EnumerateFiles( root, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly );
		}
	}
}