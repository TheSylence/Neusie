using System.Collections.Generic;
using JetBrains.Annotations;
using Neusie.Utility;
using Xunit;

namespace Neusie.Tests.Utility
{
	[UsedImplicitly]
	public class DictionaryExtensionsTests
	{
		public class Merge
		{
			[Fact]
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

				Assert.Equal( expected, dict );
			}

			[Fact]
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
				Assert.Equal( 1, dict.Count );
				Assert.Equal( 1, dict["one"] );
			}

			[Fact]
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
				Assert.Equal( 1, dict.Count );
				Assert.Equal( 3, dict["one"] );
			}
		}
	}
}