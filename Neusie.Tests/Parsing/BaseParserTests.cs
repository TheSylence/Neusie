using Neusie.Parsing;
using Xunit;

namespace Neusie.Tests.Parsing
{
	public class BaseParserTests
	{
		public class FilterCSharpSource
		{
			[Theory]
			[InlineData( "test.cs", true )]
			[InlineData( "test.xaml.cs", false )]
			[InlineData( "test.txt", false )]
			[InlineData( "test.txt.cs", true )]
			[InlineData( "test.g.cs", false )]
			[InlineData( "test.g.i.cs", false )]
			[InlineData( "test.a.b.cs", true )]
			public void ShouldFilterCorrectly( string fileName, bool expected )
			{
				// Arrange
				var sut = new BaseParserImpl();

				// Act
				var actual = sut.IsCSharpSourceFileWrapper( fileName );

				// Assert
				Assert.Equal( expected, actual );
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