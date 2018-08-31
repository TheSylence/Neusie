using System.Linq;
using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class InputConfigurationTests
	{
		[Fact]
		public void ShouldHaveAllSourcesWhenMultipleWereGiven()
		{
			// Arrange
			var expected = new[]
			{
				"C:\\path\\to\\folder",
				"D:\\solution.sln",
				"E:\\project\\file.csproj"
			};

			var section = Substitute.For<IConfigurationSection>();
			section["sources:0"].Returns( expected[0] );
			section["sources:1"].Returns( expected[1] );
			section["sources:2"].Returns( expected[2] );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Sources;

			// Assert
			Assert.Equal( expected.OrderBy( x => x ), actual.OrderBy( x => x ) );
		}

		[Fact]
		public void ShouldHaveSingleSourceWhenOnlyOneWasGiven()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			section["sources"].Returns( "single" );

			var sut = new InputConfiguration( section );

			// Act
			var actual = sut.Sources;

			// Assert
			Assert.Single( actual, "single" );
			Assert.Single( actual );
		}
	}
}