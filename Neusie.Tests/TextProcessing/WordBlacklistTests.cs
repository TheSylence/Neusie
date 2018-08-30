using System.Collections.Generic;
using System.Linq;
using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	public class WordBlacklistTests
	{
		public class Process
		{
			[Fact]
			public void ShouldOnlyRemoveBlacklistedWords()
			{
				// Arrange
				var input = new Dictionary<string, int>
				{
					{"one", 1},
					{"two", 2},
					{"three", 3}
				};

				var blackList = new[] {"two"};
				var sut = new WordBlacklist( blackList );

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.DoesNotContain( "two", actual.Keys );
				Assert.Contains( "one", actual.Keys );
				Assert.Contains( "three", actual.Keys );
			}

			[Fact]
			public void ShouldReturnInputListWhenBlacklistOnlyHasDifferentEntries()
			{
				// Arrange
				var input = new Dictionary<string, int>
				{
					{"one", 1},
					{"two", 2},
					{"three", 3}
				};

				var blackList = new[] {"four"};
				var sut = new WordBlacklist( blackList );

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( input, actual );
			}

			[Fact]
			public void ShouldReturnInputWhenBacklistIsEmpty()
			{
				// Arrange
				var input = new Dictionary<string, int>
				{
					{"one", 1},
					{"two", 2},
					{"three", 3}
				};

				var sut = new WordBlacklist( Enumerable.Empty<string>() );

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( input, actual );
			}
		}
	}
}