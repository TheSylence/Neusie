using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using QuadTrees;
using QuadTrees.QTreePointF;

namespace Neusie.Generation.Image
{
	internal class CollisionMap : ICollisionMap
	{
		public CollisionMap( int width, int height )
		{
			Width = width;
			Height = height;

			PointTree = new QuadTreePointF<PointEntry>( 0, 0, Width, Height );
		}

		private bool CheckBounds( RectangleF rect )
		{
			if( rect.Right >= Width || rect.Left >= Width )
			{
				return false;
			}

			if( rect.Left <= 0 || rect.Right <= 0 )
			{
				return false;
			}

			if( rect.Bottom <= 0 || rect.Top <= 0 )
			{
				return false;
			}

			if( rect.Bottom >= Height || rect.Top >= Height )
			{
				return false;
			}

			return true;
		}

		private bool CheckCollision( RectangleF rect )
		{
			rect.Inflate( 1, 1 );

			var rectangles = PointTree.GetObjects( rect ).Select( r => r.Rect );
			if( !CheckCollision( rect, rectangles ) )
			{
				return false;
			}

			return true;
		}

		private static bool CheckCollision( RectangleF rect, IEnumerable<RectangleF> rectangles )
		{
			foreach( var exitingRectangle in rectangles )
			{
				if( exitingRectangle.IntersectsWith( rect ) )
				{
					return false;
				}
			}

			return true;
		}

		private IEnumerable<PointEntry> GetEdgePoints( RectangleF rect )
		{
			yield return new PointEntry( rect, new PointF( rect.Left, rect.Top ) );
			yield return new PointEntry( rect, new PointF( rect.Right, rect.Top ) );
			yield return new PointEntry( rect, new PointF( rect.Left, rect.Bottom ) );
			yield return new PointEntry( rect, new PointF( rect.Right, rect.Bottom ) );
		}

		public bool Check( IEnumerable<RectangleF> rects )
		{
			return rects.All( rect => CheckBounds( rect ) && CheckCollision( rect ) );
		}

		public void Insert( IEnumerable<RectangleF> rects )
		{
			var rectList = rects.ToList();
			PointTree.AddRange( rectList.SelectMany( GetEdgePoints ) );
		}

		public IEnumerable<RectangleF> Rectangles => PointTree.GetAllObjects().Select( o => o.Rect ).Distinct();

		private readonly int Height;

		private readonly QuadTreePointF<PointEntry> PointTree;
		private readonly int Width;

		private class PointEntry : IPointFQuadStorable
		{
			public PointEntry( RectangleF rect, PointF point )
			{
				Rect = rect;
				Point = point;
			}

			/// <inheritdoc />
			public PointF Point { get; }

			public RectangleF Rect { get; }
		}
	}
}