using System.Collections.Generic;
using System.Linq;

namespace Neusie.Parsing
{
	internal class FolderParser : BaseParser, ISourceParser
	{
		public FolderParser( IDirectoryEnumerator directoryEnumerator )
		{
			DirectoryEnumerator = directoryEnumerator;
		}

		/// <inheritdoc />
		public IEnumerable<string> Files( string root )
		{
			var allFiles = DirectoryEnumerator.Files( root, "*.cs", true );
			var sourceFiles = allFiles.Where( IsCSharpSourceFile );

			return sourceFiles;
		}

		private readonly IDirectoryEnumerator DirectoryEnumerator;
	}
}