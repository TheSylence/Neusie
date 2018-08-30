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