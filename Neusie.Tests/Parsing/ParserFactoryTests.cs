using System;
using System.IO;
using Neusie.Parsing;
using Xunit;

namespace Neusie.Tests.Parsing
{
	public class ParserFactoryTests
	{
		public class Construct
		{
			[Fact]
			public void ShouldReturnValidParserWhenDirectoryIsGiven()
			{
				// Arrange
				var sut = new ParserFactory();
				var folderPath = Directory.GetCurrentDirectory();

				// Act
				var actual = sut.Construct( folderPath );

				// Assert
				Assert.NotNull( actual );
			}

			[Fact]
			public void ShouldReturnValidParserWhenProjectFileIsGiven()
			{
				// Arrange
				var sut = new ParserFactory();
				const string filePath = "project.csproj";

				// Act
				var actual = sut.Construct( filePath );

				// Assert
				Assert.NotNull( actual );
			}

			[Fact]
			public void ShouldReturnValidParserWhenSolutionIsGiven()
			{
				// Arrange
				var sut = new ParserFactory();
				const string filePath = "solution.sln";

				// Act
				var actual = sut.Construct( filePath );

				// Assert
				Assert.NotNull( actual );
			}

			[Fact]
			public void ShouldThrowWhenSourceCannotBeParsed()
			{
				// Arrange
				var sut = new ParserFactory();

				// Act
				var ex = Record.Exception( () => sut.Construct( "file.name" ) );

				// Assert
				Assert.IsType<ArgumentException>( ex );
			}
		}
	}
}