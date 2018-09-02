using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.Json;
using Neusie.Configuration;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationFactoryTests
	{
		[Fact]
		public void DefaultShouldContainCorrectValues()
		{
			// Arrange
			var sut = ConfigurationFactory.Build( new string[0] );

			// Act & Assert
			Assert.Equal( new[] {"microsoft", "system", "var"}, sut.Input.Blacklist.OrderBy( x => x ).ToArray() );
			Assert.Empty( sut.Input.Sources );
			Assert.Equal( sut.Input.MinWordLength, 2 );
			Assert.Equal( sut.Input.KeepComments, false );
			Assert.Equal( sut.Input.KeepStrings, false );
			Assert.Equal( sut.Input.KeepNamespaces, false );
		}

		public class Build
		{
			[Fact]
			public void ShouldThrowWhenConfigFileWasNotFound()
			{
				// Arrange
				var args = new[] {"config=file.name"};

				// Act
				var ex = Record.Exception( () => ConfigurationFactory.Build( args ) );

				// Assert
				Assert.IsType<FileNotFoundException>( ex );
			}
		}

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