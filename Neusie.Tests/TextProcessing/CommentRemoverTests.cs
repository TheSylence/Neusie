using System;
using JetBrains.Annotations;
using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	[UsedImplicitly]
	public class CommentRemoverTests
	{
		public class Process
		{
			[Fact]
			public void ShouldNotTouchNormalText()
			{
				// Arrange
				const string input = "hello world";
				const string expected = "hello world";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldOnlyRemoveSingleLineForSingleLineComment()
			{
				// Arrange
				var input = "// header" + Environment.NewLine + "content";
				const string expected = "content";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveBlockCommentFromBeginning()
			{
				// Arrange
				const string input = "/* comment */ test";
				const string expected = " test";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveBlockCommentFromEnd()
			{
				// Arrange
				const string input = "test /* comment*/";
				const string expected = "test ";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveBlockCommentFromMiddle()
			{
				// Arrange
				const string input = "pre /* comment */ post";
				const string expected = "pre  post";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveMultiLineComment()
			{
				// Arrange
				var input = "pre /* comment" + Environment.NewLine + "comment2 */ post";
				const string expected = "pre  post";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveSingleLineComment()
			{
				// Arrange
				const string input = "// comment";
				var expected = string.Empty;

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveSingleLineCommentAtFileEnd()
			{
				// Arrange
				var input = "// comment" + Environment.NewLine;
				var expected = string.Empty;

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldSingleLineCommentFromEnd()
			{
				// Arrange
				const string input = "test // comment";
				const string expected = "test ";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			private static string Execute( string input )
			{
				var sut = new CommentRemover();
				return sut.Process( input );
			}
		}
	}
}