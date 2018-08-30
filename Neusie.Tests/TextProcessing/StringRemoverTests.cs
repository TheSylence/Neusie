using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	public class StringRemoverTests
	{
		public class Process
		{
			[Theory]
			[InlineData( "Hello \"world\"!", "Hello !" )]
			[InlineData( "var x = @\"test\";", "var x = ;" )]
			public void ShouldOnlyRemoveStrings( string input, string expected )
			{
				// Arrange
				var sut = new StringRemover();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveStringValue()
			{
				// Arrange
				const string input = "\"hello\"";
				var expected = "";
				var sut = new StringRemover();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveVerbatimStringValue()
			{
				// Arrange
				const string input = "@\"hello\"";
				var expected = "";
				var sut = new StringRemover();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( expected, actual );
			}
		}
	}
}