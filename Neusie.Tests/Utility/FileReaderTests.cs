using System;
using System.IO;
using Neusie.Utility;
using NUnit.Framework;

namespace Neusie.Tests.Utility
{
	[TestFixture]
	internal class FileReaderTests
	{
		public class Read
		{
			[Test]
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
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldReturnNullWhenFileDoesNotExist()
			{
				// Arrange
				var sut = new FileReader();

				// Act
				var actual = sut.Read( "non.existing.file" );

				// Assert
				Assert.IsNull( actual );
			}

			[Test]
			public void ShouldThrowWhenArgumentIsNull()
			{
				// Arrange
				var sut = new FileReader();

				// Act
				TestDelegate action = () => sut.Read( null );

				// Assert
				Assert.Throws<ArgumentNullException>( action );
			}
		}
	}
}