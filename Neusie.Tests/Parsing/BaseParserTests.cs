using Neusie.Parsing;
using NUnit.Framework;

namespace Neusie.Tests.Parsing
{
	[TestFixture]
	internal class BaseParserTests
	{
		[TestFixture]
		internal class FilterCSharpSource
		{
			[Test]
			[TestCase( "test.cs", true )]
			[TestCase( "test.xaml.cs", false )]
			[TestCase( "test.txt", false )]
			[TestCase( "test.txt.cs", true )]
			[TestCase( "test.g.cs", false )]
			[TestCase( "test.g.i.cs", false )]
			[TestCase( "test.a.b.cs", true )]
			public void ShouldFilterCorrectly( string fileName, bool expected )
			{
				// Arrange
				var sut = new BaseParserImpl();

				// Act
				var actual = sut.IsCSharpSourceFileWrapper( fileName );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			private class BaseParserImpl : BaseParser
			{
				internal bool IsCSharpSourceFileWrapper( string fileName )
				{
					return IsCSharpSourceFile( fileName );
				}
			}
		}
	}
}