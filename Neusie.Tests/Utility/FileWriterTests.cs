using System;
using System.IO;
using Neusie.Utility;
using NUnit.Framework;

namespace Neusie.Tests.Utility
{
	[TestFixture]
	internal class FileWriterTests
	{
		public class Write
		{
			[Test]
			public void ShouldThrowWhenArgumentIsNull()
			{
				// Arrange
				var sut = new FileWriter();

				// Act
				TestDelegate action = () => sut.Write( null, "content" );

				// Assert
				Assert.Throws<ArgumentNullException>( action );
			}

			[Test]
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
				Assert.AreEqual( expected, actual );
			}

			[Test]
			public void ShouldWriteEmptyFileWhenContentIsNull()
			{
				// Arrange
				var sut = new FileWriter();
				var fileName = "null.file";

				// Act
				sut.Write( fileName, null );

				// Assert
				var actual = File.ReadAllText( fileName );
				Assert.IsTrue( string.IsNullOrEmpty( actual ) );
			}
		}
	}
}