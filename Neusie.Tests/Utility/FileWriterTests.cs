using System;
using System.IO;
using Neusie.Utility;
using Xunit;

namespace Neusie.Tests.Utility
{
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
				Action action = () => sut.Write( null, "content" );

				// Assert
				Assert.Throws<ArgumentNullException>( action );
			}

			[Fact]
			public void ShouldWriteContentToFile()
			{
				// Arrange
				var sut = new FileWriter();
				var fileName = "content.file";
				var expected = "Hello World";

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
				var fileName = "null.file";

				// Act
				sut.Write( fileName, null );

				// Assert
				var actual = File.ReadAllText( fileName );
				Assert.True( string.IsNullOrEmpty( actual ) );
			}
		}
	}
}