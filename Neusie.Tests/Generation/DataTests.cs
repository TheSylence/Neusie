using System;
using Neusie.Generation;
using Xunit;

namespace Neusie.Tests.Generation
{
	public class DataTests
	{
		public class FileName
		{
			[Fact]
			public void ShouldHaveCorrectExtension()
			{
				// Arrange
				var sut = new TestData( ".ext" );

				// Act
				var actual = sut.FileNameWrapper( "fileName" );

				// Assert
				Assert.Equal( "fileName.ext", actual );
			}

			private class TestData : BaseData
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