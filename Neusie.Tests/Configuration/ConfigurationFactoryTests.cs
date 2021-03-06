﻿using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.Json;
using Neusie.Configuration;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationFactoryTests
	{
		[Fact]
		public void DefaultShouldContainCorrectCsvValues()
		{
			// Arrange
			var configuration = ConfigurationFactory.Build( new string[0] );
			var sut = configuration.Output.Csv;

			// Act & Assert
			Assert.True( sut.IsEnabled );
		}

		[Fact]
		public void DefaultShouldContainCorrectImageValues()
		{
			// Arrange
			var configuration = ConfigurationFactory.Build( new string[0] );
			var sut = configuration.Output.Image;

			// Act & Assert
			Assert.True( sut.IsEnabled );
			Assert.Equal( 10, sut.MinimumFontSize );
			Assert.Equal( 250, sut.Compactness );
			Assert.Equal( 1024, sut.Height );
			Assert.Equal( 1024, sut.Width );
			Assert.Equal( "Tahoma", sut.Font );
		}

		[Fact]
		public void DefaultShouldContainCorrectInputValues()
		{
			// Arrange
			var configuration = ConfigurationFactory.Build( new string[0] );
			var sut = configuration.Input;

			// Act & Assert
			Assert.Equal( new[] {"microsoft", "system", "var"}, sut.Blacklist.OrderBy( x => x ).ToArray() );
			Assert.Empty( sut.Sources );
			Assert.Equal( 2, sut.MinWordLength );
			Assert.False( sut.KeepComments );
			Assert.False( sut.KeepStrings );
			Assert.False( sut.KeepNamespaces );
		}

		[Fact]
		public void DefaultShouldContainCorrectOutputValues()
		{
			// Arrange
			var configuration = ConfigurationFactory.Build( new string[0] );
			var sut = configuration.Output;

			// Act & Assert
			Assert.Equal( Directory.GetCurrentDirectory(), sut.TargetPath );
			Assert.Equal( "noisemap", sut.TargetName );
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