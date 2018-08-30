using JetBrains.Annotations;
using Neusie.Utility;
using Xunit;

namespace Neusie.Tests.Utility
{
	[UsedImplicitly]
	public class ObjectExtensionsTests
	{
		public class Yield
		{
			[Fact]
			public void ShouldContainElement()
			{
				// Arrange
				var expected = new object();

				// Act
				var actual = expected.Yield();

				// Assert
				Assert.Contains( expected, actual );
			}
		}
	}
}