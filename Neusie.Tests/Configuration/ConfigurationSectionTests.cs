using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationSectionTests
	{
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
				Assert.True( actual.HasValue );
				Assert.Equal( expected, actual.Value );
			}

			[Fact]
			public void ShouldReturnNullWhenKeyWasNotFound()
			{
				// Arrange
				var section = Substitute.For<IConfigurationSection>();
				var sut = new SectionBaseWrapper( section );

				// Act
				var actual = sut.ReadIntWrapper( "non.existing" );

				// Assert
				Assert.Null( actual );
			}

			[Fact]
			public void ShouldReturnNullWhenValueCannotBeParsed()
			{
				// Arrange
				const string key = "key";

				var section = Substitute.For<IConfigurationSection>();
				section[key].Returns( "abc" );
				var sut = new SectionBaseWrapper( section );

				// Act
				var actual = sut.ReadIntWrapper( key );

				// Assert
				Assert.Null( actual );
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
			public void ShouldReturnNullWhenKeyWasNotFound()
			{
				// Arrange
				var section = Substitute.For<IConfigurationSection>();
				var sut = new SectionBaseWrapper( section );

				// Act
				var actual = sut.ReadStringWrapper( "non.existing" );

				// Assert
				Assert.Null( actual );
			}
		}

		private class SectionBaseWrapper : ConfigurationSectionBase
		{
			/// <inheritdoc />
			public SectionBaseWrapper( IConfigurationSection section ) : base( section )
			{
			}

			public int? ReadIntWrapper( string key )
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