using System.Drawing;

namespace Neusie.Generation.Image
{
	internal class WordPlacement
	{
		public WordPlacement( string word, PointF position, int fontSize )
		{
			Word = word;
			Position = position;
			FontSize = fontSize;
		}

		public int FontSize { get; }
		public PointF Position { get; }
		public string Word { get; }
	}
}