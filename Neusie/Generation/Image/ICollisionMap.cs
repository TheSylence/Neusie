using System.Collections.Generic;
using System.Drawing;

namespace Neusie.Generation.Image
{
	internal interface ICollisionMap
	{
		bool Check( IEnumerable<RectangleF> rects );
		void Insert( IEnumerable<RectangleF> rects );
	}
}