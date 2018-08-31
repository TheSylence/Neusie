using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class ConfigurationSectionTests
	{
		public class ReadString
		{
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

			public string ReadStringWrapper( string key )
			{
				return ReadString( key );
			}
		}
	}
}