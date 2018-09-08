using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

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

		private static bool CheckBlock( int bx, int by, byte[] image, int imageWidth, int imageHeight )
		{
			for( var x = 0; x < BlockSize && x < imageWidth; ++x )
			{
				for( var y = 0; y < BlockSize && y < imageHeight; ++y )
				{
					var xx = bx + x;
					var yy = by + y;
					if( xx >= imageWidth || yy >= imageHeight )
					{
						continue;
					}

					var idx = ( yy * imageWidth + xx ) * 4;
					if( image[idx] != 0 )
					{
						return true;
					}
				}
			}

			return false;
		}

		private static IEnumerable<RectangleF> ComputeBoundingRectangles( GraphicsPath path, PointF offset )
		{
			var bounds = path.GetBounds();
			var h = Math.Max( (int)bounds.Height, BlockSize );
			var w = Math.Max( (int)bounds.Width, BlockSize );

			using( var image = new Bitmap( w, h ) )
			{
				using( var gfx = Graphics.FromImage( image ) )
				{
					var m = new Matrix();
					m.Translate( -bounds.X + 1, -bounds.Y + 1 );
					path.Transform( m );
					gfx.FillPath( new SolidBrush( Color.White ), path );
				}

				var imageWidth = image.Width;
				var imageHeight = image.Height;

				var bitmapData = image.LockBits( new Rectangle( 0, 0, imageWidth, imageHeight ), ImageLockMode.ReadOnly, image.PixelFormat );
				var ptr = bitmapData.Scan0;
				var numBytes = bitmapData.Stride * imageHeight;
				var bytes = new byte[numBytes];
				Marshal.Copy( ptr, bytes, 0, numBytes );

				for( var bx = 0; bx < w; bx += BlockSize )
				{
					for( var by = 0; by < h; by += BlockSize )
					{
						if( CheckBlock( bx, by, bytes, imageWidth, imageHeight ) )
						{
							yield return new RectangleF( bx + offset.X, by + offset.Y, BlockSize, BlockSize );
						}
					}
				}
			}
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

					rects.AddRange( ComputeBoundingRectangles( path, bound.Location ) );
				}
			}

			var offset = GetAlignmentOffset( rects );
			return new StringMeasurement( word, rects, offset );
		}

		internal const float RectSpacing = 0f;

		private const int BlockSize = 12;

		internal static readonly StringFormat StringFormat;
	}
}