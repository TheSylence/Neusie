using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationSectionTests
	{
		public class ReadBool
		{
			[Theory]
			[InlineData( true )]
			[InlineData( false )]
			public void ShouldReturnCorrectValueWhenFound( bool expected )
			{
				// Arrange
				const string key = "key";

				var section = Substitute.For<IConfigurationSection>();
				section[key].Returns( expected.ToString() );

				var sut = new SectionBaseWrapper( section );

				// Act
				var actual = sut.ReadBoolWrapper( key );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldThrowWhenValueCannotBeParsed()
			{
				// Arrange
				const string key = "key";

				var section = Substitute.For<IConfigurationSection>();
				section[key].Returns( "abc" );

				var sut = new SectionBaseWrapper( section );

				// Act
				var ex = Record.Exception( () => sut.ReadBoolWrapper( key ) );

				// Assert
				Assert.IsType<FormatException>( ex );
			}

			[Fact]
			public void ShouldThrowWhenValueWasNotSet()
			{
				// Arrange
				var section = Substitute.For<IConfigurationSection>();
				var sut = new SectionBaseWrapper( section );

				// Act
				var ex = Record.Exception( () => sut.ReadBoolWrapper( "non.existing" ) );

				// Assert
				Assert.IsType<KeyNotFoundException>( ex );
			}
		}

		public class ReadInt
		{
			[Fact]
			public void ShouldReadValueFromSection()
			{
				// Arrange
				const int expected = 123;
				const string key = "key";

				var section = Substitute.For<IConfigurationSection>();
				section[key].Returns( "123" );
				var sut = new SectionBaseWrapper( section );

				// Act
				var actual = sut.ReadIntWrapper( key );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldThrowWhenKeyWasNotFound()
			{
				// Arrange
				var section = Substitute.For<IConfigurationSection>();
				var sut = new SectionBaseWrapper( section );

				// Act
				var ex = Record.Exception( () => sut.ReadIntWrapper( "non.existing" ) );

				// Assert
				Assert.IsType<KeyNotFoundException>( ex );
			}

			[Fact]
			public void ShouldThrowWhenValueCannotBeParsed()
			{
				// Arrange
				const string key = "key";

				var section = Substitute.For<IConfigurationSection>();
				section[key].Returns( "abc" );
				var sut = new SectionBaseWrapper( section );

				// Act
				var ex = Record.Exception( () => sut.ReadIntWrapper( key ) );

				// Assert
				Assert.IsType<FormatException>( ex );
			}
		}

		public class ReadString
		{
			[Fact]
			public void ShouldReadValueFromSection()
			{
				// Arrange
				const string expected = "value";
				const string key = "key";

				var section = Substitute.For<IConfigurationSection>();
				section[key].Returns( expected );
				var sut = new SectionBaseWrapper( section );

				// Act
				var actual = sut.ReadStringWrapper( key );

				// Assert
				Assert.Equal( expected, actual );
			}

			[Fact]
			public void ShouldThrowWhenKeyWasNotFound()
			{
				// Arrange
				var section = Substitute.For<IConfigurationSection>();
				var sut = new SectionBaseWrapper( section );

				// Act
				var ex = Record.Exception( () => sut.ReadStringWrapper( "non.existing" ) );

				// Assert
				Assert.IsType<KeyNotFoundException>( ex );
			}
		}

		private class SectionBaseWrapper : ConfigurationSectionBase
		{
			/// <inheritdoc />
			public SectionBaseWrapper( IConfigurationSection section ) : base( section )
			{
			}

			public bool ReadBoolWrapper( string key )
			{
				return ReadBool( key );
			}

			public int ReadIntWrapper( string key )
			{
				return ReadInt( key );
			}

			public string ReadStringWrapper( string key )
			{
				return ReadString( key );
			}
		}
	}
}