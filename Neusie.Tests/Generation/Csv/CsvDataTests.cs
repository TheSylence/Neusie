using System.IO;
using Neusie.Generation.Csv;
using Xunit;

namespace Neusie.Tests.Generation.Csv
{
	public class CsvDataTests
	{
		public class Save
		{
			[Fact]
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
				Assert.Equal( expected, actual );
			}
		}

		public new class ToString
		{
			[Fact]
			public void ShouldContainStringData()
			{
				// Arrange
				var expected = "hello world";
				var sut = new CsvData( expected, "ext" );

				// Act
				var actual = sut.ToString();

				// Assert
				Assert.Equal( expected, actual );
			}
		}
	}
}