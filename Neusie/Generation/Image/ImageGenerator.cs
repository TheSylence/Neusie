using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;

namespace Neusie.Generation.Image
{
	internal class ImageGenerator : IGenerator
	{
		public ImageGenerator( IWordPlacer placer, int width, int height, FontFamily fontFamily )
		{
			Placer = placer;
			Width = width;
			Height = height;
			FontFamily = fontFamily;

			Rand = new Random();

			LoadColors();
		}

		private void LoadColors()
		{
			ColorMap.AddRange( new[]
			{
				Color.Green,
				Color.CornflowerBlue,
				Color.Gray,
				Color.IndianRed,
				Color.Orange,
				Color.YellowGreen,
				Color.CadetBlue,
				Color.Beige,
				Color.RosyBrown,
				Color.SandyBrown,
				Color.DarkKhaki
			} );
		}

		/// <inheritdoc />
		public IData Generate( Dictionary<string, int> words )
		{
			var wordList = words.OrderByDescending( w => w.Value );
			var placements = Placer.Place( wordList );

			var img = new Bitmap( Width, Height );
			using( var gfx = Graphics.FromImage( img ) )
			{
				gfx.TextRenderingHint = TextRenderingHint.AntiAlias;
				gfx.CompositingQuality = CompositingQuality.HighQuality;
				gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
				gfx.SmoothingMode = SmoothingMode.AntiAlias;

				gfx.Clear( Color.Black );

				foreach( var wordPlacement in placements )
				{
					using( var path = new GraphicsPath() )
					{
						var matrix = new Matrix();
						matrix.Translate( wordPlacement.Position.X, wordPlacement.Position.Y );

						path.AddString( wordPlacement.Word, FontFamily,
							(int)FontStyle.Regular, wordPlacement.FontSize,
							Point.Empty, StringMeasurer.StringFormat );
						path.Transform( matrix );

						var color = ColorMap[Rand.Next( 0, ColorMap.Count )];

						gfx.DrawPath( new Pen( color ), path );
						gfx.FillPath( new SolidBrush( color ), path );
					}
				}
			}

			return new ImageData( img );
		}

		private readonly List<Color> ColorMap = new List<Color>();
		private readonly FontFamily FontFamily;
		private readonly int Height;
		private readonly IWordPlacer Placer;
		private readonly Random Rand;
		private readonly int Width;
	}
}