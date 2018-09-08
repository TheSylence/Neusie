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
		private static WordExtractor BuildWordExtractor( InputConfiguration config )
		{
			var preProcessors = new List<ITextPreProcessor>
			{
				new LineEndingNormalizer()
			};

			if( !config.KeepComments )
			{
				preProcessors.Add( new CommentRemover() );
			}

			if( !config.KeepStrings )
			{
				preProcessors.Add( new StringRemover() );
			}

			if( !config.KeepNamespaces )
			{
				preProcessors.Add( new NamespaceCleaner() );
			}

			var postProcessors = new ITextPostProcessor[]
			{
				new WordBlacklist( config.Blacklist ),
				new ShortWordRemover( config.MinWordLength )
			};

			var extractor = new WordExtractor( preProcessors, postProcessors );
			return extractor;
		}

		private static void GenerateCsv( CsvOutputConfiguration config, Dictionary<string, int> words, string baseName )
		{
			if( !config.IsEnabled )
			{
				return;
			}

			Console.Write( "Writing word list to csv..." );
			var csvGenerator = new CsvGenerator();
			var csvData = csvGenerator.Generate( words );
			csvData.Save( baseName );
			Console.WriteLine( "[Done]" );
		}

		private static void GenerateImage( ImageOutputConfiguration config, Dictionary<string, int> words, string baseName, int seed )
		{
			if( !config.IsEnabled )
			{
				return;
			}

			Console.Write( "Generating cloud image..." );
			var width = config.Width;
			var height = config.Height;
			var rand = new Random( seed );
			var fontFamily = new FontFamily( config.Font );

			var collisionMap = new CollisionMap( width, height );
			var measurer = new StringMeasurer();

			var minimumFontSize = config.MinimumFontSize;
			var compactness = config.Compactness;
			var placer = new WordPlacer( measurer, collisionMap, width, height, rand, fontFamily, minimumFontSize, compactness );
			var imageGenerator = new ImageGenerator( placer, width, height, fontFamily );

			var image = imageGenerator.Generate( words );
			image.Save( baseName );

			Console.WriteLine( "[Done]" );
		}

		private static void Main( string[] args )
		{
			var config = ConfigurationFactory.Build( args );

			Console.Write( "Searching for files..." );
			var parser = new FolderParser( new DirectoryEnumerator() );
			var sourceFiles = parser.Files( config.Input.Sources.First() ).ToList();
			Console.WriteLine( "[Done]" );

			var extractor = BuildWordExtractor( config.Input );
			var fileReader = new FileReader();

			var words = new Dictionary<string, int>();

			var sourceFileContents = sourceFiles.Select( sourceFile => fileReader.Read( sourceFile ) );
			var sourceFileWords = sourceFileContents.AsParallel().Select( text => extractor.Extract( text ) );

			Console.Write( $"Extracting words from {sourceFiles.Count} source files..." );
			foreach( var wordsInFile in sourceFileWords )
			{
				words.Merge( wordsInFile );
			}

			Console.WriteLine( "[Done]" );

			var baseName = Path.Combine( config.Output.TargetPath, "noiseMap" );

			GenerateCsv( config.Output.Csv, words, baseName );
			GenerateImage( config.Output.Image, words, baseName, config.Output.Seed );
		}
	}
}