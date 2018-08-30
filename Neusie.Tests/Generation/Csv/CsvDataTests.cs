using System.IO;
using JetBrains.Annotations;
using Neusie.Generation.Csv;
using Xunit;

namespace Neusie.Tests.Generation.Csv
{
	[UsedImplicitly]
	public class CsvDataTests
	{
		public class Save
		{
			[Fact]
			public void ShouldWriteContentToFile()
			{
				// Arrange
				const string baseName = "fileName";
				const string fileName = "fileName.ext";
				const string expected = "hello world";
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
				const string expected = "hello world";
				var sut = new CsvData( expected, "ext" );

				// Act
				var actual = sut.ToString();

				// Assert
				Assert.Equal( expected, actual );
			}
		}
	}
}