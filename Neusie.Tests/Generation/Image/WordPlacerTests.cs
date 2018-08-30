using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Neusie.Generation.Image;
using NSubstitute;
using Xunit;

namespace Neusie.Tests.Generation.Image
{
	public class WordPlacerTests
	{
		public class Place
		{
			[Fact]
			public void ShouldContainAllGivenWords()
			{
				// Arrange
				var words = new List<KeyValuePair<string, int>>
				{
					new KeyValuePair<string, int>( "three", 3 ),
					new KeyValuePair<string, int>( "two", 2 ),
					new KeyValuePair<string, int>( "one", 1 )
				};

				var rand = new Random( 123 );

				var map = Substitute.For<ICollisionMap>();
				map.Check( Arg.Any<IEnumerable<RectangleF>>() ).Returns( true );

				var measurer = Substitute.For<IStringMeasurer>();
				measurer.Measure( Arg.Any<string>(), Arg.Any<Font>() )
					.Returns( inf => new StringMeasurement( inf.ArgAt<string>( 0 ), new []{ RectangleF.Empty}, PointF.Empty ) );

				var sut = new WordPlacer( measurer, map, 100, 100, rand, FontFamily.GenericMonospace );

				// Act
				var actual = sut.Place( words ).ToList();

				// Assert
				Assert.Equal( words.Count, actual.Count );
			}
		}
	}
}