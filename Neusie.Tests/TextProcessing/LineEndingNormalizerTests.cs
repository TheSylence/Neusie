using System;
using JetBrains.Annotations;
using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	[UsedImplicitly]
	public class LineEndingNormalizerTests
	{
		public class Process
		{
			[Fact]
			public void ShouldKeepWindowsLineEnding()
			{
				// Arrange
				const string input = "1\r\n2";
				var expected = "1" + Environment.NewLine + "2";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldNormalizeLineEndings()
			{
				// Arrange
				const string input = "1\r2\n3\r\n4";
				var expected = "1" + Environment.NewLine + "2" + Environment.NewLine + "3" + Environment.NewLine + "4";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldReplaceMacLineEnding()
			{
				// Arrange
				const string input = "1\r2";
				var expected = "1" + Environment.NewLine + "2";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldReplaceUnixLineEnding()
			{
				// Arrange
				const string input = "1\n2";
				var expected = "1" + Environment.NewLine + "2";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			private static string Execute( string input )
			{
				var sut = new LineEndingNormalizer();
				return sut.Process( input );
			}
		}
	}
}