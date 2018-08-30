using System;
using System.IO;
using JetBrains.Annotations;
using Neusie.Utility;
using Xunit;

namespace Neusie.Tests.Utility
{
	[UsedImplicitly]
	public class FileReaderTests
	{
		public class Read
		{
			[Fact]
			public void ShouldReturnFileContents()
			{
				// Arrange
				var fileName = Path.GetTempFileName();
				const string expected = "hello, world";
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
				void Action() => sut.Read( null );

				// Assert
				Assert.Throws<ArgumentNullException>( (Action)Action );
			}
		}
	}
}