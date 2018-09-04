using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ImageOutputConfigurationTests
	{
		[Fact]
		public void ShouldBeDisabledWhenSpecified()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section[ConfigurationKeys.ImageOutput.Enabled].Returns( "false" );

			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.IsEnabled;

			// Assert
			Assert.False( actual );
		}

		[Fact]
		public void ShouldBeEnabledByDefault()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.IsEnabled;

			// Assert
			Assert.True( actual );
		}

		[Fact]
		public void ShouldHaveCorrectFontFamilyWhenNoneIsGiven()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.Font;

			// Assert
			const string expected = "Tahoma";
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveCorrectHeightWhenNoneIsGiven()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.Height;

			// Assert
			const int expected = 1024;
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveCorrectWidthWhenNoneIsGiven()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();

			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.Width;

			// Assert
			const int expected = 1024;
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveSpecifiedFont()
		{
			// Arrange
			const string expected = "Arial";
			var section = Substitute.For<IConfigurationSection>();
			section[ConfigurationKeys.ImageOutput.Font].Returns( expected );
			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.Font;

			// Assert
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveSpecifiedHeight()
		{
			// Arrange
			const int expected = 123;
			var section = Substitute.For<IConfigurationSection>();
			section[ConfigurationKeys.ImageOutput.Height].Returns( expected.ToString() );
			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.Height;

			// Assert
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void ShouldHaveSpecifiedWidth()
		{
			// Arrange
			const int expected = 123;
			var section = Substitute.For<IConfigurationSection>();
			section[ConfigurationKeys.ImageOutput.Width].Returns( expected.ToString() );
			var sut = new ImageOutputConfiguration( section );

			// Act
			var actual = sut.Width;

			// Assert
			Assert.Equal( expected, actual );
		}
	}
}