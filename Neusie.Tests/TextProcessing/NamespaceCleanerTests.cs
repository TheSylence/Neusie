using System;
using Neusie.TextProcessing;
using NUnit.Framework;

namespace Neusie.Tests.TextProcessing
{
	[TestFixture]
	internal class NamespaceCleanerTests
	{
		[TestFixture]
		internal class Process
		{
			[Test]
			[TestCase( "\t\tusing( disposable )" )]
			[TestCase( "var namespaceName = test" )]
			[TestCase( "Using()" )]
			[TestCase( "Namespace()" )]
			public void ShouldLeaveInlineWordsIntact( string input )
			{
				// Arrange
				var sut = new NamespaceCleaner();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( input, actual );
			}

			[Test]
			public void ShouldRemoveNamespaceDeclarationLines()
			{
				// Arrange
				var input = "namespace Name.Space" + Environment.NewLine + "public class" + Environment.NewLine + "Test";
				var expected = "public class" + Environment.NewLine + "Test";
				var sut = new NamespaceCleaner();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldRemoveUsingDirectives()
			{
				// Arrange
				var input = "using System;" + Environment.NewLine + "using System.Text;" + Environment.NewLine + "public class" + Environment.NewLine + "Test";
				var expected = "public class" + Environment.NewLine + "Test";
				var sut = new NamespaceCleaner();

				// Act
				var actual = sut.Process( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}
		}
	}
}