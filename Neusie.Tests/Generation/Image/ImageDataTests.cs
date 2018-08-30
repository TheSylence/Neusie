using System.Collections;
using System.Drawing;
using JetBrains.Annotations;
using Neusie.Generation.Image;
using Xunit;

namespace Neusie.Tests.Generation.Image
{
	[UsedImplicitly]
	public class ImageDataTests
	{
		public class Save
		{
			[Fact]
			public void ShouldCreateImageWithCorrectContent()
			{
				// Arrange
				const string baseName = "correctContent";
				const string fileName = "correctContent.png";

				using( var referenceImage = new Bitmap( 32, 32 ) )
				{
					for( var x = 0; x < 32; ++x )
					for( var y = 0; y < 32; ++y )
					{
						referenceImage.SetPixel( x, y, Color.CornflowerBlue );
					}

					for( var i = 0; i < 32; ++i )
					{
						referenceImage.SetPixel( i, i, Color.IndianRed );
					}

					var sut = new ImageData( referenceImage );

					// Act
					sut.Save( baseName );

					// Assert
					using( var gdiBitmap = System.Drawing.Image.FromFile( fileName ) )
					{
						IEqualityComparer comp = new ImageComparer();
						Assert.True( comp.Equals( referenceImage, gdiBitmap ) );
					}
				}
			}

			[Fact]
			public void ShouldCreateImageWithCorrectSizes()
			{
				// Arrange
				const string baseName = "correctSizes";
				const string fileName = "correctSizes.png";

				using( var gdiBitmap = new Bitmap( 32, 16 ) )
				{
					var sut = new ImageData( gdiBitmap );

					// Act
					sut.Save( baseName );
				}

				// Assert
				using( var gdiBitmap = System.Drawing.Image.FromFile( fileName ) )
				{
					Assert.Equal( 32, gdiBitmap.Width );
					Assert.Equal( 16, gdiBitmap.Height );
				}
			}
		}
	}
}