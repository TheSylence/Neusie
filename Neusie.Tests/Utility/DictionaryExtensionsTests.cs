using System.Collections.Generic;
using Neusie.Utility;
using NUnit.Framework;

namespace Neusie.Tests.Utility
{
	[TestFixture]
	internal class DictionaryExtensionsTests
	{
		[TestFixture]
		internal class Merge
		{
			[Test]
			public void ShouldAddNewEntriesWithTheirValues()
			{
				// Arrange
				var dict = new Dictionary<string, int>
				{
					{"one", 1}
				};

				var other = new Dictionary<string, int>
				{
					{"two", 2},
					{"three", 3}
				};

				// Act
				dict.Merge( other );

				// Assert
				var expected = new Dictionary<string, int>
				{
					{"one", 1},
					{"two", 2},
					{"three", 3}
				};

				CollectionAssert.AreEquivalent( expected, dict );
			}

			[Test]
			public void ShouldDoNothingWhenOtherIsEmpty()
			{
				// Arrange
				var dict = new Dictionary<string, int>
				{
					{"one", 1}
				};

				var other = new Dictionary<string, int>();

				// Act
				dict.Merge( other );

				// Assert
				Assert.AreEqual( 1, dict.Count );
				Assert.AreEqual( 1, dict["one"] );
			}

			[Test]
			public void ShouldIncreseValueOfExistingEntries()
			{
				// Arrange
				var dict = new Dictionary<string, int>
				{
					{"one", 1}
				};
				var other = new Dictionary<string, int>
				{
					{"one", 2}
				};

				// Act
				dict.Merge( other );

				// Assert
				Assert.AreEqual( 1, dict.Count );
				Assert.AreEqual( 3, dict["one"] );
			}
		}
	}
}