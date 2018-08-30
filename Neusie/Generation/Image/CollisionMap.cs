using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Neusie.Generation.Image
{
	internal class CollisionMap
	{
		public CollisionMap( int width, int height )
		{
			Width = width;
			Height = height;

			_Rectangles.Add( new RectangleF( 0, 0, Width, 1 ) );
			_Rectangles.Add( new RectangleF( 0, 0, 1, Height ) );
			_Rectangles.Add( new RectangleF( Width - 1, 0, 1, Height ) );
			_Rectangles.Add( new RectangleF( 0, Height - 1, Width, 1 ) );
		}

		public bool Check( RectangleF rect )
		{
			return CheckBounds( rect ) && CheckCollision( rect );
		}

		public void Insert( RectangleF rect )
		{
			_Rectangles.Add( rect );
		}

		private bool CheckBounds( RectangleF rect )
		{
			if( rect.Right >= Width )
			{
				return false;
			}

			if( rect.Left <= 0 )
			{
				return false;
			}

			if( rect.Bottom <= 0 )
			{
				return false;
			}

			if( rect.Top >= Height )
			{
				return false;
			}

			return true;
		}

		private bool CheckCollision( RectangleF rect )
		{
			foreach( var exitingRectangle in _Rectangles )
			{
				if(exitingRectangle.IntersectsWith( rect ) )
				{
					return false;
				}
			}

			return true;
		}

		public IEnumerable<RectangleF> Rectangles => _Rectangles.Skip( 4 );

		private readonly List<RectangleF> _Rectangles = new List<RectangleF>();

		private readonly int Height;
		private readonly int Width;
	}
}