using System;
using System.IO;

namespace Neusie.Parsing
{
	internal class ParserFactory
	{
		internal ISourceParser Construct( string source )
		{
			if( Directory.Exists( source ) )
			{
				return new FolderParser( new DirectoryEnumerator() );
			}

			var ext = Path.GetExtension( source );

			switch( ext )
			{
			case ".sln":
				return new SolutionParser();

			case ".csproj":
				return new ProjectParser();
			}

			throw new ArgumentException( "Source cannot be parsed" );
		}
	}
}