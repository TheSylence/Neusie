using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class InputConfigurationTests
	{
		[Fact]
		public void ShouldContainCombinedBlacklist()
		{
			// Arrange
			File.WriteAllText( "blacklist.file", "three" + Environment.NewLine + "four" );

			var expected = new[] {"one", "two", "three", "four"};

			var section = Substitute.For<IConfigurationSection>();
			section["blacklist:0"].Returns( "one" );
			section["blacklist:1"].Returns( "two" );
			section["blacklistfile"].Returns( "blacklist.file" );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Blacklist;

			// Assert
			Assert.Equal( expected.OrderBy( x => x ), actual.OrderBy( x => x ) );
		}

		[Fact]
		public void ShouldContainShortWordFilterFromConfig()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section["minwordlength"].Returns( "4" );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.MinWordLength;

			// Assert
			Assert.Equal( 4, actual );
		}

		[Fact]
		public void ShouldHaveAllBlacklistWordsWhenMultipleWereGiven()
		{
			// Arrange
			var expected = new[] {"one", "two"};

			var section = Substitute.For<IConfigurationSection>();
			section["blacklist:0"].Returns( "one" );
			section["blacklist:1"].Returns( "two" );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Blacklist;

			// Assert
			Assert.Equal( expected.OrderBy( x => x ), actual.OrderBy( x => x ) );
		}

		[Fact]
		public void ShouldHaveAllSourcesWhenMultipleWereGiven()
		{
			// Arrange
			var expected = new[]
			{
				"C:\\path\\to\\folder",
				"D:\\solution.sln",
				"E:\\project\\file.csproj"
			};

			var section = Substitute.For<IConfigurationSection>();
			section["sources:0"].Returns( expected[0] );
			section["sources:1"].Returns( expected[1] );
			section["sources:2"].Returns( expected[2] );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Sources;

			// Assert
			Assert.Equal( expected.OrderBy( x => x ), actual.OrderBy( x => x ) );
		}

		[Fact]
		public void ShouldHaveSingleBlacklistEntryWhenOnlyOneWasGiven()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section["blacklist"].Returns( "single" );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Blacklist;

			// Assert
			Assert.Single( actual, "single" );
			Assert.Single( actual );
		}

		[Fact]
		public void ShouldHaveSingleSourceWhenOnlyOneWasGiven()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section["sources"].Returns( "single" );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Sources;

			// Assert
			Assert.Single( actual, "single" );
			Assert.Single( actual );
		}

		[Fact]
		public void ShouldThrowWhenBlacklistFileWasNotFound()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section["blacklistfile"].Returns( "non.existing.file" );

			var sut = new InputConfiguration( section );

			// Act
			var ex = Record.Exception( () => sut.Blacklist );

			// Assert
			Assert.IsType<FileNotFoundException>( ex );
		}
	}
}