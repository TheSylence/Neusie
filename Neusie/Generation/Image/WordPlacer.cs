using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Neusie.Generation.Image
{
	internal interface IWordPlacer
	{
		IEnumerable<WordPlacement> Place( IEnumerable<KeyValuePair<string, int>> words );
	}

	internal class WordPlacer : IWordPlacer
	{
		public WordPlacer( IStringMeasurer measurer, ICollisionMap map, int width, int height, Random rand, FontFamily fontFamily )
		{
			Measurer = measurer;
			Map = map;
			Width = width;
			Height = height;
			Rand = rand;
			FontFamily = fontFamily;
		}

		public IEnumerable<WordPlacement> Place( IEnumerable<KeyValuePair<string, int>> words )
		{
			var fontSize = Height;

			foreach( var word in words )
			{
				fontSize = (int)Math.Min( fontSize, 100 * Math.Log10( word.Value + 100 ) );

				while( fontSize > 5 )
				{
					var found = false;

					for( var i = 0; i < 150; ++i )
					{
						var inf = Measurer.Measure( word.Key, new Font( FontFamily, fontSize ) );
						var rects = inf.Rectangles.ToList();

						var maxW = Math.Max( 1, Width - (int)inf.Rectangles.Max( ii => ii.Width ) );
						var maxH = Math.Max( 1, Height - (int)inf.Rectangles.Max( ii => ii.Height ) );

						var xOffset = inf.Offset.X + Rand.Next( 1, maxW );
						var yOffset = inf.Offset.Y + Rand.Next( 1, maxH );

						var newRects = new List<RectangleF>( rects.Count );

						foreach( var r in rects )
						{
							r.Offset( xOffset, yOffset );
							newRects.Add( r );
						}

						if( !Map.Check( newRects ) )
						{
							continue;
						}

						Map.Insert( newRects );

						yield return new WordPlacement( word.Key, new PointF( xOffset, yOffset ), fontSize );
						found = true;
						break;
					}

					if( found )
					{
						break;
					}

					fontSize--;
				}
			}
		}

		private readonly FontFamily FontFamily;
		private readonly int Height;
		private readonly ICollisionMap Map;

		private readonly IStringMeasurer Measurer;
		private readonly Random Rand;
		private readonly int Width;
	}
}