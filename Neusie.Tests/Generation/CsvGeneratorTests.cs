using System;
using System.Collections.Generic;
using Neusie.Generation;
using NUnit.Framework;

namespace Neusie.Tests.Generation
{
	[TestFixture]
	internal class CsvGeneratorTests
	{
		[TestFixture]
		internal class Generate
		{
			[Test]
			public void ShouldBeEmptyWhenWordsAreEmpty()
			{
				// Arrange
				var sut = new CsvGenerator();
				var input = new Dictionary<string, int>();

				// Act
				var actual = sut.Generate( input );

				// Assert
				var expected = "Word;Count" + Environment.NewLine;
				Assert.AreEqual( expected, actual.ToString() );
			}

			[Test]
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
				Assert.AreEqual( expected, actual.ToString() );
			}
		}
	}
}