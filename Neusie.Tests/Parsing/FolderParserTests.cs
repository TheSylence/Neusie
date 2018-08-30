using Neusie.Parsing;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Parsing
{
	public class FolderParserTests
	{
		public class ReadFiles
		{
			[Fact]
			public void ShouldContainAllFoldersInRootFolder()
			{
				// Arrange
				var enumerator = Substitute.For<IDirectoryEnumerator>();
				var expected = new[] {"1.cs", "2.cs"};
				enumerator.Files( "root", "*.cs", true ).Returns( expected );

				var sut = new FolderParser( enumerator );

				// Act
				var actual = sut.Files( "root" );

				// Assert
				Assert.Equal( expected, actual );
			}
		}
	}
}