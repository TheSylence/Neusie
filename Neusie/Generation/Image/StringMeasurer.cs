using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Neusie.Generation.Image
{
	internal class StringMeasurer : IStringMeasurer
	{
		static StringMeasurer()
		{
			StringFormat = new StringFormat
			{
				FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoFontFallback | StringFormatFlags.NoWrap,
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			};
		}

		public StringMeasurement Measure( string word, Font font )
		{
			var rects = new List<RectangleF>( word.Length );

			var processedChars = "";

			foreach( var c in word )
			{
				float currentRight;

				processedChars += c.ToString();
				using( var path = new GraphicsPath() )
				{
					path.AddString( processedChars, font.FontFamily, (int)font.Style, font.SizeInPoints, Point.Empty, StringFormat );
					var bound = path.GetBounds();
					bound.Inflate( 2 * RectSpacing, 2 * RectSpacing );

					currentRight = bound.Right;
				}

				using( var path = new GraphicsPath() )
				{
					path.AddString( c.ToString(), font.FontFamily, (int)font.Style, font.SizeInPoints, Point.Empty, StringFormat );

					var bound = path.GetBounds();
					var rightOffset = currentRight - bound.Right;
					bound.Offset( -RectSpacing + rightOffset, -RectSpacing );
					bound.Inflate( 2 * RectSpacing, 2 * RectSpacing );

					rects.Add( bound );
				}
			}

			var offset = GetAlignmentOffset( rects );
			return new StringMeasurement( word, rects, offset );
		}

		private static PointF GetAlignmentOffset( IReadOnlyCollection<RectangleF> rects )
		{
			if( !rects.Any() )
			{
				return PointF.Empty;
			}

			var xOffset = rects.Min( r => r.X );
			var yOffset = rects.Min( r => r.Y );

			return new PointF( -xOffset, -yOffset );
		}

		internal const float RectSpacing = 0f;

		internal static readonly StringFormat StringFormat;
	}
}