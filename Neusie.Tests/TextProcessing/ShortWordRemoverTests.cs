using System.Collections.Generic;
using Neusie.TextProcessing;
using NUnit.Framework;

namespace Neusie.Tests.TextProcessing
{
	[TestFixture]
	internal class ShortWordRemoverTests
	{
		[TestFixture]
		internal class Process
		{
			[Test]
			public void ShouldNotTouchLongWords()
			{
				// Arrange
				var input = new Dictionary<string, int>
				{
					{"one", 1},
					{"four", 2},
					{"verylongword", 3}
				};

				var sut = new ShortWordRemover( 2 );

				// Act
				var actual = sut.Process( input );

				// Assert
				CollectionAssert.AreEquivalent( input, actual );
			}

			[Test]
			public void ShouldRemoveAllWordsShorterThanThreshold()
			{
				// Arrange
				var input = new Dictionary<string, int>
				{
					{"one", 1},
					{"four", 2},
					{"verylongword", 3}
				};

				var sut = new ShortWordRemover( 4 );

				// Act
				var actual = sut.Process( input );

				// Assert
				CollectionAssert.Contains( actual.Keys, "four" );
				CollectionAssert.Contains( actual.Keys, "verylongword" );
				CollectionAssert.DoesNotContain( actual.Keys, "one" );
			}
		}
	}
}