using Neusie.Parsing;
using NSubstitute;
using NUnit.Framework;

namespace Neusie.Tests.Parsing
{
	[TestFixture]
	internal class FolderParserTests
	{
		[TestFixture]
		internal class ReadFiles
		{
			[Test]
			public void ShouldContainAllFoldersInRootFolder()
			{
				// Arrange
				var enumerator = Substitute.For<IDirectoryEnumerator>();
				var expected = new[] { "1.cs", "2.cs" };
				enumerator.Files("root", "*.cs", true).Returns(expected);

				var sut = new FolderParser(enumerator);

				// Act
				var actual = sut.Files("root");

				// Assert
				CollectionAssert.AreEquivalent(expected, actual);
			}
		}
	}
}