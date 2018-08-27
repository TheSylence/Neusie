using System.Collections.Generic;
using System.Linq;
using Neusie.TextProcessing;
using NUnit.Framework;

namespace Neusie.Tests.TextProcessing
{
	[TestFixture]
	internal class WordBlacklistTests
	{
		[TestFixture]
		internal class Process
		{
			[Test]
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
				CollectionAssert.DoesNotContain( actual.Keys, "two" );
				CollectionAssert.Contains( actual.Keys, "one" );
				CollectionAssert.Contains( actual.Keys, "three" );
			}

			[Test]
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
				CollectionAssert.AreEquivalent( input, actual );
			}

			[Test]
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
				CollectionAssert.AreEquivalent( input, actual );
			}
		}
	}
}