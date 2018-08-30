using System;
using System.IO;
using JetBrains.Annotations;
using Neusie.Utility;
using Xunit;

namespace Neusie.Tests.Utility
{
	[UsedImplicitly]
	public class FileWriterTests
	{
		public class Write
		{
			[Fact]
			public void ShouldThrowWhenArgumentIsNull()
			{
				// Arrange
				var sut = new FileWriter();

				// Act
				void Action() => sut.Write( null, "content" );

				// Assert
				Assert.Throws<ArgumentNullException>( (Action)Action );
			}

			[Fact]
			public void ShouldWriteContentToFile()
			{
				// Arrange
				var sut = new FileWriter();
				const string fileName = "content.file";
				const string expected = "Hello World";

				// Act
				sut.Write( fileName, expected );

				// Assert
				var actual = File.ReadAllText( fileName );
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldWriteEmptyFileWhenContentIsNull()
			{
				// Arrange
				var sut = new FileWriter();
				const string fileName = "null.file";

				// Act
				sut.Write( fileName, null );

				// Assert
				var actual = File.ReadAllText( fileName );
				Assert.True( string.IsNullOrEmpty( actual ) );
			}
		}
	}
}