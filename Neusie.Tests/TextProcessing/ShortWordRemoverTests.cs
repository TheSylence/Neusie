using System.Collections.Generic;
using JetBrains.Annotations;
using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	[UsedImplicitly]
	public class ShortWordRemoverTests
	{
		public class Process
		{
			[Fact]
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
				Assert.Equal( input, actual );
			}

			[Fact]
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
				Assert.Contains( "four", actual.Keys );
				Assert.Contains( "verylongword", actual.Keys );
				Assert.DoesNotContain( "one", actual.Keys );
			}
		}
	}
}