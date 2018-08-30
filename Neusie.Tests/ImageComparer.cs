using System.Collections.Generic;
using System.Drawing;

namespace Neusie.Tests
{
	internal class ImageComparer : EqualityComparer<Bitmap>
	{
		/// <inheritdoc />
		public override bool Equals( Bitmap imgA, Bitmap imgB )
		{
			var w = imgA.Width;
			var h = imgA.Height;

			if( imgB.Width != w || imgB.Height != h )
			{
				return false;
			}

			for( var x = 0; x < w; ++x )
			{
				for( var y = 0; y < h; ++y )
				{
					var a = imgA.GetPixel( x, y );
					var b = imgB.GetPixel( x, y );

					if( !a.Equals( b ) )
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <inheritdoc />
		public override int GetHashCode( Bitmap obj )
		{
			return obj.GetHashCode();
		}
	}
}