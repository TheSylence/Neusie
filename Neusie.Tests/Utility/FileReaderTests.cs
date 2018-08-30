using System;
using System.IO;
using Neusie.Utility;
using Xunit;

namespace Neusie.Tests.Utility
{
	public class FileReaderTests
	{
		public class Read
		{
			[Fact]
			public void ShouldReturnFileContents()
			{
				// Arrange
				var fileName = Path.GetTempFileName();
				var expected = "hello, world";
				File.WriteAllText( fileName, expected );

				var sut = new FileReader();

				// Act
				var actual = sut.Read( fileName );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldReturnNullWhenFileDoesNotExist()
			{
				// Arrange
				var sut = new FileReader();

				// Act
				var actual = sut.Read( "non.existing.file" );

				// Assert
				Assert.Null( actual );
			}

			[Fact]
			public void ShouldThrowWhenArgumentIsNull()
			{
				// Arrange
				var sut = new FileReader();

				// Act
				Action action = () => sut.Read( null );

				// Assert
				Assert.Throws<ArgumentNullException>( action );
			}
		}
	}
}