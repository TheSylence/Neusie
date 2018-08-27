using System;
using Neusie.Generation;
using NUnit.Framework;

namespace Neusie.Tests.Generation
{
	[TestFixture]
	internal class DataTests
	{
		[TestFixture]
		internal class FileName
		{
			[Test]
			public void ShouldHaveCorrectExtension()
			{
				// Arrange
				var sut = new TestData( ".ext" );

				// Act
				var actual = sut.FileNameWrapper( "fileName" );

				// Assert
				Assert.AreEqual( "fileName.ext", actual );
			}

			private class TestData : Data
			{
				/// <inheritdoc />
				public TestData( string extension ) : base( extension )
				{
				}

				public string FileNameWrapper( string baseName )
				{
					return FileName( baseName );
				}

				/// <inheritdoc />
				public override void Save( string baseName )
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}