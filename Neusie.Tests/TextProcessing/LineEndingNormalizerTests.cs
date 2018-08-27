using System;
using Neusie.TextProcessing;
using NUnit.Framework;

namespace Neusie.Tests.TextProcessing
{
	[TestFixture]
	internal class LineEndingNormalizerTests
	{
		[TestFixture]
		internal class Process
		{
			[Test]
			public void ShouldNormalizeLineEndings()
			{
				// Arrange
				var input = "1\r2\n3\r\n4";
				var expected = "1" + Environment.NewLine + "2" + Environment.NewLine + "3" + Environment.NewLine + "4";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldKeepWindowsLineEnding()
			{
				// Arrange
				var input = "1\r\n2";
				var expected = "1" + Environment.NewLine + "2";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldReplaceMacLineEnding()
			{
				// Arrange
				var input = "1\r2";
				var expected = "1" + Environment.NewLine + "2";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldReplaceUnixLineEnding()
			{
				// Arrange
				var input = "1\n2";
				var expected = "1" + Environment.NewLine + "2";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.AreEqual( expected, actual );
			}

			private string Execute( string input )
			{
				var sut = new LineEndingNormalizer();
				return sut.Process( input );
			}
		}
	}
}