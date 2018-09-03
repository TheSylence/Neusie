using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Neusie.Configuration;
using Neusie.Generation.Csv;
using Neusie.Generation.Image;
using Neusie.Parsing;
using Neusie.TextProcessing;
using Neusie.Utility;

[assembly: InternalsVisibleTo( "Neusie.Tests" )]
[assembly: InternalsVisibleTo( "DynamicProxyGenAssembly2" )]

namespace Neusie
{
	[PublicAPI]
	public class Program
	{
		private static void Main( string[] args )
		{
			var config = ConfigurationFactory.Build( args );

			Console.Write( "Searching for files..." );
			var parser = new FolderParser( new DirectoryEnumerator() );
			var sourceFiles = parser.Files( config.Input.Sources.First() ).ToList();
			Console.WriteLine( "[Done]" );

			var preProcessors = new List<ITextPreProcessor>
			{
				new LineEndingNormalizer()
			};

			if( !config.Input.KeepComments )
			{
				preProcessors.Add( new CommentRemover() );
			}

			if( !config.Input.KeepStrings )
			{
				preProcessors.Add( new StringRemover() );
			}

			if( !config.Input.KeepNamespaces )
			{
				preProcessors.Add( new NamespaceCleaner() );
			}

			var postProcessors = new ITextPostProcessor[]
			{
				new WordBlacklist( config.Input.Blacklist ),
				new ShortWordRemover( config.Input.MinWordLength )
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

			var baseName = Path.Combine( config.Input.Sources.First(), "noiseMap" );

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
