using System;
using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	public class NamespaceCleanerTests
	{
		public class Process
		{
			[Theory]
			[InlineData( "\t\tusing( disposable )" )]
			[InlineData( "var namespaceName = test" )]
			[InlineData( "Using()" )]
			[InlineData( "Namespace()" )]
			public void ShouldLeaveInlineWordsIntact( string input )
			{
				// Arrange
				var sut = new NamespaceCleaner();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( input, actual );
			}

			[Fact]
			public void ShouldRemoveNamespaceDeclarationLines()
			{
				// Arrange
				var input = "namespace Name.Space" + Environment.NewLine + "public class" + Environment.NewLine + "Test";
				var expected = "public class" + Environment.NewLine + "Test";
				var sut = new NamespaceCleaner();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveUsingDirectives()
			{
				// Arrange
				var input = "using System;" + Environment.NewLine + "using System.Text;" + Environment.NewLine + "public class" + Environment.NewLine + "Test";
				var expected = "public class" + Environment.NewLine + "Test";
				var sut = new NamespaceCleaner();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.Equal( expected, actual );
			}
		}
	}
}