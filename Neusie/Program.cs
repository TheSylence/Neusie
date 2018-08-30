using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Neusie.Generation.Csv;
using Neusie.Generation.Image;
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

			var baseName = Path.Combine( args[0], "noiseMap" );

			Console.Write( "Writing word list to csv..." );
			var csvGenerator = new CsvGenerator();
			var csvData = csvGenerator.Generate( words );
			csvData.Save( baseName );
			Console.WriteLine( "[Done]" );

			Console.Write( "Generating cloud image..." );
			var width = 1024;
			var height = 1024;
			var rand = new Random();
			var fontFamily = new FontFamily( "Tahoma" );

			var collisionMap = new CollisionMap( width, height );
			var measurer = new StringMeasurer();
			var placer = new WordPlacer( measurer, collisionMap, width, height, rand, fontFamily );
			var imageGenerator = new ImageGenerator( placer, width, height, fontFamily );

			var image = imageGenerator.Generate( words );
			image.Save( baseName );

			Console.WriteLine( "[Done]" );
		}
	}
}