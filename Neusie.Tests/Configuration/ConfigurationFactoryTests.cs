using System.Linq;
using Microsoft.Extensions.Configuration.Json;
using Neusie.Configuration;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationFactoryTests
	{
		public class CreateRoot
		{
			[Fact]
			public void ShouldContainFileProviderWhenSpecifiedInCommandLineArguments()
			{
				// Arrange
				var args = new[] {"config=file.name"};

				// Act
				var actual = ConfigurationFactory.CreateRoot( args, true );

				// Assert
				Assert.Equal( 3, actual.Providers.Count() );
				Assert.NotNull( actual.Providers.OfType<JsonConfigurationProvider>().SingleOrDefault() );
			}
		}
	}
}