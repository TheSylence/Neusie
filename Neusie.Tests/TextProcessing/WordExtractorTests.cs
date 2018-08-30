using System.Collections.Generic;
using System.Linq;
using Neusie.TextProcessing;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	public class WordExtractorTests
	{
		public class Extract
		{
			[Fact]
			public void ShouldApplyPostProcessorsInCorrectOrder()
			{
				// Arrange
				var d1 = new Dictionary<string, int>();
				var d2 = new Dictionary<string, int>();

				var p1 = Substitute.For<ITextPostProcessor>();
				p1.Process( Arg.Any<Dictionary<string, int>>() ).Returns( d1 );
				var p2 = Substitute.For<ITextPostProcessor>();
				p2.Process( d1 ).Returns( d2 );

				var sut = new WordExtractor( new[] {p1, p2} );

				// Act
				var actual = sut.Extract( "hello world" );

				// Assert
				Assert.Same( d2, actual );

				p1.Received( 1 ).Process( Arg.Any<Dictionary<string, int>>() );
				p2.Received( 1 ).Process( d1 );
			}

			[Fact]
			public void ShouldApplyProcessorsInCorrectOrder()
			{
				// Arrange
				var p1 = Substitute.For<ITextPreProcessor>();
				p1.Process( "zero" ).Returns( "one" );
				var p2 = Substitute.For<ITextPreProcessor>();
				p2.Process( "one" ).Returns( "two" );

				var sut = new WordExtractor( new[] {p1, p2} );

				// Act
				var _ = sut.Extract( "zero" );

				// Assert
				p1.Received( 1 ).Process( "zero" );
				p2.Received( 1 ).Process( "one" );
			}

			[Fact]
			public void ShouldCallPostProcessors()
			{
				// Arrange
				var p1 = Substitute.For<ITextPostProcessor>();
				var p2 = Substitute.For<ITextPostProcessor>();
				var sut = new WordExtractor( Enumerable.Empty<ITextPreProcessor>(), new[] {p1, p2} );

				// Act
				var _ = sut.Extract( "hello, world" );

				// Assert
				p1.Received().Process( Arg.Any<Dictionary<string, int>>() );
				p2.Received().Process( Arg.Any<Dictionary<string, int>>() );
			}

			[Fact]
			public void ShouldCallPreProcessors()
			{
				// Arrange
				var p1 = Substitute.For<ITextPreProcessor>();
				var p2 = Substitute.For<ITextPreProcessor>();
				var sut = new WordExtractor( new[] {p1, p2} );

				// Act
				var _ = sut.Extract( "hello, world" );

				// Assert
				p1.Received().Process( Arg.Any<string>() );
				p2.Received().Process( Arg.Any<string>() );
			}

			[Fact]
			public void ShouldReturnEmptyDictionaryWhenInputIsEmpty()
			{
				// Arrange
				var input = "";
				var sut = new WordExtractor();

				// Act
				var actual = sut.Extract( input );

				// Assert
				Assert.Empty( actual );
			}
		}

		public class ExtractWithoutProcessing
		{
			[Fact]
			public void ShouldContainAllWordsFromInput()
			{
				// Arrange
				var input = "one two three";
				var expected = new Dictionary<string, int>
				{
					{"one", 1}, {"two", 1}, {"three", 1}
				};

				var sut = new WordExtractor();

				// Act
				var actual = sut.ExtractWithoutProcessing( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldContainWordsWithCorrectFrequency()
			{
				// Arrange
				var input = "one two two three three three";
				var expected = new Dictionary<string, int>
				{
					{"one", 1}, {"two", 2}, {"three", 3}
				};

				var sut = new WordExtractor();

				// Act
				var actual = sut.ExtractWithoutProcessing( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldContainWordsWithCorrectFrequencyIgnoringCase()
			{
				// Arrange
				var input = "one TWO two three thRee ThrEE";
				var expected = new Dictionary<string, int>
				{
					{"one", 1}, {"two", 2}, {"three", 3}
				};

				var sut = new WordExtractor();

				// Act
				var actual = sut.ExtractWithoutProcessing( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldNotContainSpecialCharactersInWords()
			{
				// Arrange
				var input = "#region !not nullable?";
				var expected = new Dictionary<string, int>
				{
					{"region", 1}, {"not", 1}, {"nullable", 1}
				};

				var sut = new WordExtractor();

				// Act
				var actual = sut.ExtractWithoutProcessing( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Theory]
			[InlineData( "12312 1231" )]
			[InlineData( "__-- _###'" )]
			public void ShouldRemoveDigitOnlyWords( string input )
			{
				// Arrange
				var sut = new WordExtractor();

				// Act
				var actual = sut.ExtractWithoutProcessing( input );

				// Assert
				Assert.Empty( actual );
			}

			[Fact]
			public void ShouldReturnEmptyDictionaryWhenInputIsEmpty()
			{
				// Arrange
				var sut = new WordExtractor();

				// Act
				var actual = sut.ExtractWithoutProcessing( string.Empty );

				// Assert
				Assert.Empty( actual );
			}
		}
	}
}