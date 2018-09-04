using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class OutputConfigurationTests
	{
		[Fact]
		public void ShouldCorrectSeed()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section[ConfigurationKeys.Output.Seed].Returns( "123" );

			var sut = new OutputConfiguration( section );

			// Act
			var actual = sut.Seed;

			// Assert
			Assert.Equal( 123, actual );
		}

		[Fact]
		public void ShouldHaveCorrectTargetName()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			const string expected = "the name";
			section[ConfigurationKeys.Output.TargetName].Returns( expected );

			var sut = new OutputConfiguration( section );

			// Act
			var actual = sut.TargetName;

			// Assert
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveCorrectTargetPath()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			const string expected = "the target";
			section[ConfigurationKeys.Output.TargetPath].Returns( expected );

			var sut = new OutputConfiguration( section );

			// Act
			var actual = sut.TargetPath;

			// Assert
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveRandomSeedWhenNoneIsSpecified()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			var sut = new OutputConfiguration( section );

			// Act
			var seed1 = sut.Seed;
			var seed2 = sut.Seed;

			// Assert
			Assert.NotEqual( seed1, seed2 );
		}

		[Fact]
		public void ShouldReadCsvSection()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			// Act
			var sut = new OutputConfiguration( section );
			var actual = sut.Csv;

			// Assert
			section.Received( 1 ).GetSection( ConfigurationKeys.CsvOutputSection );

			Assert.NotNull( actual );
		}

		[Fact]
		public void ShouldReadImageSection()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			// Act
			var sut = new OutputConfiguration( section );
			var actual = sut.Image;

			// Assert
			section.Received( 1 ).GetSection( ConfigurationKeys.ImageOutputSection );

			Assert.NotNull( actual );
		}
	}
}