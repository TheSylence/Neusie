using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationTests
	{
		[Fact]
		public void ShouldReadInputSection()
		{
			// Arrange
			var cfg = Substitute.For<IConfiguration>();

			// Act
			var _ = new Neusie.Configuration.Configuration( cfg );

			// Assert
			cfg.Received( 1 ).GetSection( ConfigurationKeys.InputSection );
		}
	}
}