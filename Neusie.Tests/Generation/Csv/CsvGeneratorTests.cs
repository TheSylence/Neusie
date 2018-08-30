using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Neusie.Generation.Csv;
using Xunit;

namespace Neusie.Tests.Generation.Csv
{
	[UsedImplicitly]
	public class CsvGeneratorTests
	{
		public class Generate
		{
			[Fact]
			public void ShouldBeEmptyWhenWordsAreEmpty()
			{
				// Arrange
				var sut = new CsvGenerator();
				var input = new Dictionary<string, int>();

				// Act
				var actual = sut.Generate( input );

				// Assert
				var expected = "Word;Count" + Environment.NewLine;
				Assert.Equal( expected, actual.ToString() );
			}

			[Fact]
			public void ShouldContainWordsInCorrectOrder()
			{
				// Arrange
				var sut = new CsvGenerator();
				var input = new Dictionary<string, int>
				{
					{"two", 2},
					{"three", 3},
					{"one", 1}
				};

				// Act
				var actual = sut.Generate( input );

				// Assert
				var expected = "Word;Count" + Environment.NewLine +
				               "three;3" + Environment.NewLine +
				               "two;2" + Environment.NewLine +
				               "one;1" + Environment.NewLine;
				Assert.Equal( expected, actual.ToString() );
			}
		}
	}
}