using Microsoft.Extensions.Configuration;
using Neusie.Configuration;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Configuration
{
	public class OutputConfigurationTests
	{
		[Fact]
		public void ShouldHaveCorrectTargetPath()
		{
			// Arrange
			var section = Substitute.For<IConfigurationSection>();
			const string expected = "the target";
			section["targetpath"].Returns( expected );

			var sut = new OutputConfiguration( section );

			// Act
			var actual = sut.TargetPath;

			// Assert
			Assert.Equal( expected, actual );
		}
	}
}