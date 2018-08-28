using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Neusie.Generation;
using Neusie.Parsing;
using Neusie.TextProcessing;
using Neusie.Utility;

[assembly: InternalsVisibleTo( "Neusie.Tests" )]
[assembly: InternalsVisibleTo( "DynamicProxyGenAssembly2" )]

namespace Neusie
{
	internal class Program
	{
		private static void Main( string[] args )
		{
			Console.Write( "Searching for files..." );
			var parser = new FolderParser( new DirectoryEnumerator() );
			var sourceFiles = parser.Files( args[0] ).ToList();
			Console.WriteLine( "[Done]" );

			var preProcessors = new ITextPreProcessor[]
			{
				new LineEndingNormalizer(),
				new CommentRemover(),
				new StringRemover(),
				new NamespaceCleaner()
			};

			var postProcessors = new ITextPostProcessor[]
			{
				new WordBlacklist( new[] {"microsoft", "system", "var"} ),
				new ShortWordRemover( 2 )
			};

			var extractor = new WordExtractor( preProcessors, postProcessors );
			var fileReader = new FileReader();

			var words = new Dictionary<string, int>();

			var sourceFileContents = sourceFiles.Select( sourceFile => fileReader.Read( sourceFile ) );
			var sourceFileWords = sourceFileContents.Select( text => extractor.Extract( text ) );

			Console.Write( $"Extracting words from {sourceFiles.Count} source files..." );
			foreach( var wordsInFile in sourceFileWords )
			{
				words.Merge( wordsInFile );
			}

			Console.WriteLine( "[Done]" );

			Console.Write( "Writing word list to csv..." );
			var generator = new CsvGenerator();
			var csvData = generator.Generate( words );
			var baseName = Path.Combine( args[0], "noiseMap" );
			csvData.Save( baseName );
			Console.WriteLine( "[Done]" );
		}
	}
}