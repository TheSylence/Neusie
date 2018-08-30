using System;
using Neusie.TextProcessing;
using Xunit;

namespace Neusie.Tests.TextProcessing
{
	public class CommentRemoverTests
	{
		public class Process
		{
			[Fact]
			public void ShouldNotTouchNormalText()
			{
				// Arrange
				var input = "hello world";
				var expected = "hello world";

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
				var expected = "content";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveBlockCommentFromBeginning()
			{
				// Arrange
				var input = "/* comment */ test";
				var expected = " test";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveBlockCommentFromEnd()
			{
				// Arrange
				var input = "test /* comment*/";
				var expected = "test ";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveBlockCommentFromMiddle()
			{
				// Arrange
				var input = "pre /* comment */ post";
				var expected = "pre  post";

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
				var expected = "pre  post";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldRemoveSingleLineComment()
			{
				// Arrange
				var input = "// comment";
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
				var input = "test // comment";
				var expected = "test ";

				// Act
				var actual = Execute( input );

				// Assert
				Assert.Equal( expected, actual );
			}

			private string Execute( string input )
			{
				var sut = new CommentRemover();
				return sut.Process( input );
			}
		}
	}
}