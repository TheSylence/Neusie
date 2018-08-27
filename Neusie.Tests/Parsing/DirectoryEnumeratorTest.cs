using System.IO;
using System.Linq;
using Neusie.Parsing;
using NUnit.Framework;

namespace Neusie.Tests.Parsing
{
	[TestFixture]
	internal class DirectoryEnumeratorTest
	{
		[TestFixture]
		internal class Files
		{
			[Test]
			public void ShouldContainAllFilesOfNestedFolders()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.*", true );

				// Assert
				var expected = new[] {"a.txt", Path.Combine( "sub", "b.txt" ), Path.Combine( "sub2", "c.txt" ), "d.ext"}
					.Select( x => Path.Combine( dir, x ) ).ToList();
				CollectionAssert.AreEquivalent( expected, actual );

				RemoveDirectory( dir );
			}

			[Test]
			public void ShouldContainAllFilesOfRoot()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.*", false );

				// Assert
				var expected = new[] {"a.txt", "d.ext"}.Select( x => Path.Combine( dir, x ) ).ToList();
				CollectionAssert.AreEquivalent( expected, actual );

				RemoveDirectory( dir );
			}

			[Test]
			public void ShouldNotContainFilesOfNestedFolderWhenRecursiveIsFalse()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.*", false );

				// Assert
				var notExpected = new[] {Path.Combine( "sub", "b.txt" ), Path.Combine( "sub2", "c.txt" )}.Select( x => Path.Combine( dir, x ) ).ToList();
				CollectionAssert.IsNotSubsetOf( notExpected, actual );

				RemoveDirectory( dir );
			}

			[Test]
			public void ShouldOnlyContainFilesMatchingPattern()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.ext", true );

				// Assert
				var expected = new[] {"d.ext"}.Select( x => Path.Combine( dir, x ) ).ToList();
				CollectionAssert.AreEquivalent( expected, actual );

				RemoveDirectory( dir );
			}

			private string CreateDirectory( string name = null )
			{
				var dirName = TestContext.CurrentContext.Test.ClassName + "_" + TestContext.CurrentContext.Test.MethodName;
				if( name != null )
				{
					dirName += "_" + name;
				}

				var path = Path.Combine( TestContext.CurrentContext.WorkDirectory, dirName );

				Directory.CreateDirectory( path );

				Touch( path, "a.txt" );
				Touch( Path.Combine( path, "sub" ), "b.txt" );
				Touch( Path.Combine( path, "sub2" ), "c.txt" );
				Touch( path, "d.ext" );

				return path;
			}

			private void RemoveDirectory( string dir )
			{
				Directory.Delete( dir, true );
			}

			private void Touch( string dir, string file )
			{
				var path = Path.Combine( dir, file );
				Directory.CreateDirectory( dir );
				using( File.OpenWrite( path ) )
				{
				}
			}
		}
	}
}