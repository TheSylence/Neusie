using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class CsvOutputConfigationTests
	{
		[Fact]
		public void ShouldBeDisabledWhenSpecified()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section[ConfigurationKeys.CsvOutput.Enabled].Returns( "false" );

			var sut = new CsvOutputConfiguration( section );

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

			var sut = new CsvOutputConfiguration( section );

			// Act
			var actual = sut.IsEnabled;

			// Assert
			Assert.True( actual );
		}
	}
}