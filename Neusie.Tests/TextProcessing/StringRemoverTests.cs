using Neusie.TextProcessing;
using NUnit.Framework;

namespace Neusie.Tests.TextProcessing
{
	[TestFixture]
	internal class StringRemoverTests
	{
		[TestFixture]
		internal class Process
		{
			[Test]
			[TestCase( "Hello \"world\"!", "Hello !" )]
			[TestCase("var x = @\"test\";", "var x = ;")]
			public void ShouldOnlyRemoveStrings( string input, string expected )
			{
				// Arrange
				var sut = new StringRemover();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldRemoveStringValue()
			{
				// Arrange
				const string input = "\"hello\"";
				var expected = "";
				var sut = new StringRemover();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldRemoveVerbatimStringValue()
			{
				// Arrange
				const string input = "@\"hello\"";
				var expected = "";
				var sut = new StringRemover();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}
		}
	}
}