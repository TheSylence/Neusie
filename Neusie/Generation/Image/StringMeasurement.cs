using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Neusie.Generation.Image
{
	internal class StringMeasurement
	{
		public StringMeasurement( string word, IEnumerable<RectangleF> rectangles, PointF offset )
		{
			Word = word;
			Offset = offset;
			Rectangles = rectangles.ToList();
		}

		public IReadOnlyCollection<RectangleF> Rectangles { get; }
		public string Word { get; }
		public PointF Offset { get; }
	}
}