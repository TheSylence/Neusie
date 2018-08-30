using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Neusie.Parsing;
using Xunit;

namespace Neusie.Tests.Parsing
{
	[UsedImplicitly]
	public class DirectoryEnumeratorTest
	{
		public class Files
		{
			[Fact]
			public void ShouldContainAllFilesOfNestedFolders()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.*", true ).OrderBy( x => x ).ToList();

				// Assert
				var expected = new[] {"a.txt", Path.Combine( "sub", "b.txt" ), Path.Combine( "sub2", "c.txt" ), "d.ext"}
					.Select( x => Path.Combine( dir, x ) ).OrderBy( x => x ).ToList();
				Assert.Equal( expected, actual );

				RemoveDirectory( dir );
			}

			[Fact]
			public void ShouldContainAllFilesOfRoot()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.*", false );

				// Assert
				var expected = new[] {"a.txt", "d.ext"}.Select( x => Path.Combine( dir, x ) ).ToList();
				Assert.Equal( expected, actual );

				RemoveDirectory( dir );
			}

			[Fact]
			public void ShouldNotContainFilesOfNestedFolderWhenRecursiveIsFalse()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.*", false ).ToList();

				// Assert
				var notExpected = new[] {Path.Combine( "sub", "b.txt" ), Path.Combine( "sub2", "c.txt" )}.Select( x => Path.Combine( dir, x ) ).ToList();

				foreach( var x in notExpected )
				{
					Assert.DoesNotContain( x, actual );
				}

				RemoveDirectory( dir );
			}

			[Fact]
			public void ShouldOnlyContainFilesMatchingPattern()
			{
				// Arrange
				var dir = CreateDirectory();
				var sut = new DirectoryEnumerator();

				// Act
				var actual = sut.Files( dir, "*.ext", true );

				// Assert
				var expected = new[] {"d.ext"}.Select( x => Path.Combine( dir, x ) ).ToList();
				Assert.Equal( expected, actual );

				RemoveDirectory( dir );
			}

			private static string CreateDirectory( [CallerMemberName] string name = null )
			{
				var path = "DirectoryEnumeratorTest_" + name;

				Directory.CreateDirectory( path );

				Touch( path, "a.txt" );
				Touch( Path.Combine( path, "sub" ), "b.txt" );
				Touch( Path.Combine( path, "sub2" ), "c.txt" );
				Touch( path, "d.ext" );

				return path;
			}

			private static void RemoveDirectory( string dir )
			{
				Directory.Delete( dir, true );
			}

			private static void Touch( string dir, string file )
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