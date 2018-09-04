using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JetBrains.Annotations;
using Neusie.Generation.Image;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Generation.Image
{
	[UsedImplicitly]
	public class ImageGeneratorTests
	{
		public class Generate
		{
			[Fact]
			public void ShouldDrawNonBlackPixelsWhenPlacingAWord()
			{
				// Arrange
				const string baseName = "nonBlackPixels";
				const string fileName = "nonBlackPixels.png";

				var expectedPlacement = new[]
				{
					new WordPlacement( "one", new PointF( 10, 10 ), 20 )
				};

				var placer = Substitute.For<IWordPlacer>();
				placer.Place( Arg.Any<IEnumerable<KeyValuePair<string, int>>>() ).Returns( expectedPlacement );

				var sut = new ImageGenerator( placer, 100, 100, FontFamily.GenericMonospace );
				var words = new Dictionary<string, int>
				{
					{"one", 1}
				};

				// Act
				var data = sut.Generate( words );

				// Assert
				data.Save( baseName );

				using( var img = (Bitmap)System.Drawing.Image.FromFile( fileName ) )
				{
					var charsInWord = "one".Length;
					var blackPixels = CountNonBlackPixels( img );
					Assert.True( blackPixels > charsInWord );
				}
			}

			[Fact]
			public void ShouldPlaceWordsInFrequencyOrder()
			{
				// Arrange
				var placer = Substitute.For<IWordPlacer>();

				var sut = new ImageGenerator( placer, 100, 100, FontFamily.GenericMonospace );
				var words = new Dictionary<string, int>
				{
					{"one", 1},
					{"two", 2},
					{"three", 3}
				};

				// Act
				sut.Generate( words );

				// Assert

				placer.Received( 1 ).Place( Arg.Is<IEnumerable<KeyValuePair<string, int>>>( pairs => IsOrderedWithCountOfThree( pairs ) ) );
			}

			private static int CountNonBlackPixels( Bitmap img )
			{
				var counter = 0;
				for( var x = 0; x < img.Width; ++x )
				for( var y = 0; y < img.Height; ++y )
				{
					var p = img.GetPixel( x, y );
					if( p.R != 0 || p.G != 0 || p.B != 0 )
					{
						++counter;
					}
				}

				return counter;
			}

			private static bool IsOrderedWithCountOfThree( IEnumerable<KeyValuePair<string, int>> pairs )
			{
				var keyValuePairs = pairs.ToList();

				return keyValuePairs.Count == 3
				       && keyValuePairs[0].Value == 3
				       && keyValuePairs[1].Value == 2
				       && keyValuePairs[2].Value == 1;
			}
		}
	}
}