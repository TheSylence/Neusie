using System.IO;
using Neusie.Generation.Csv;
using NUnit.Framework;

namespace Neusie.Tests.Generation.Csv
{
	[TestFixture]
	internal class CsvDataTests
	{
		[TestFixture]
		internal class Save
		{
			[Test]
			public void ShouldWriteContentToFile()
			{
				// Arrange
				const string baseName = "fileName";
				string fileName = "fileName.ext";
				var expected = "hello world";
				var sut = new CsvData( expected, "ext" );

				// Act
				sut.Save( baseName );

				// Assert
				var actual = File.ReadAllText( fileName );
				Assert.AreEqual( expected, actual );
			}
		}

		[TestFixture]
		internal new class ToString
		{
			[Test]
			public void ShouldContainStringData()
			{
				// Arrange
				var expected = "hello world";
				var sut = new CsvData( expected, "ext" );

				// Act
				var actual = sut.ToString();

				// Assert
				Assert.AreEqual( expected, actual );
			}
		}
	}
}