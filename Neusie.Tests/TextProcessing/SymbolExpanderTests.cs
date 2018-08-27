using Neusie.TextProcessing;
using NUnit.Framework;

namespace Neusie.Tests.TextProcessing
{
	[TestFixture]
	internal class SymbolExpanderTests
	{
		[TestFixture]
		internal class Process
		{
			[Test]
			public void ShouldReplaceAllSymbolsWithSpacedVariants()
			{
				// Arrange
				var symbols = new[]
				{
					'<', '>', '{', '}', '(', ')', '/', '\\', '.', ',', ';', '\"', '\'', ']',
					'[', '#', '=', '_', '-', ':', '!', '?'
				};

				string expected = string.Empty;
				string input = string.Empty;

				foreach( var symbol in symbols )
				{
					input += symbol.ToString();
					expected += " " + symbol + " ";
				}

				var sut = new SymbolExpander();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			[TestCase( "#region", " # region" )]
			[TestCase( "<tag>", " < tag > " )]
			public void ShouldReplaceSymbolAroundWords( string input, string expected )
			{
				// Arrange
				var sut = new SymbolExpander();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}
		}
	}
}