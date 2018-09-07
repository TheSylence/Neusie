using System.Collections.Generic;
using System.Linq;

namespace Neusie.TextProcessing
{
	internal class WordExtractor
	{
		public WordExtractor()
			: this( Enumerable.Empty<ITextPreProcessor>(), Enumerable.Empty<ITextPostProcessor>() )
		{
		}

		public WordExtractor( IEnumerable<ITextPreProcessor> preProcessors )
			: this( preProcessors, Enumerable.Empty<ITextPostProcessor>() )
		{
		}

		public WordExtractor( IEnumerable<ITextPostProcessor> postProcessors )
			: this( Enumerable.Empty<ITextPreProcessor>(), postProcessors )
		{
		}

		public WordExtractor( IEnumerable<ITextPreProcessor> preProcessors, IEnumerable<ITextPostProcessor> postProcessors )
		{
			PreProcessors = preProcessors.ToList();
			PostProcessors = postProcessors.ToList();
		}

		public Dictionary<string, int> Extract( string text )
		{
			foreach( var preProcessor in PreProcessors )
			{
				text = preProcessor.Process( text );
			}

			var result = ExtractWithoutProcessing( text );

			foreach( var postProcessor in PostProcessors )
			{
				result = postProcessor.Process( result );
			}

			return result;
		}

		internal static Dictionary<string, int> ExtractWithoutProcessing( string text )
		{
			var words = Split( text ).Where( IsWord );

			return words.GroupBy( x => x.ToLower() ).ToDictionary( x => x.Key, x => x.Count() );
		}

		private static bool IsWord( string str )
		{
			return str.Any( char.IsLetter );
		}

		private static IEnumerable<string> Split( string text )
		{
			var buffer = new List<char>();

			foreach( var c in text )
			{
				if( char.IsPunctuation( c ) )
				{
					continue;
				}

				if( char.IsWhiteSpace( c ) )
				{
					if( buffer.Count > 0 )
					{
						yield return new string( buffer.ToArray() );
						buffer.Clear();
					}

					continue;
				}

				buffer.Add( c );
			}

			if( buffer.Count > 0 )
			{
				yield return new string( buffer.ToArray() );
			}
		}

		private readonly List<ITextPostProcessor> PostProcessors;
		private readonly List<ITextPreProcessor> PreProcessors;
	}
}