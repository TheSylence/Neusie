using System.Drawing;

namespace Neusie.Generation.Image
{
	internal interface IStringMeasurer
	{
		StringMeasurement Measure( string word, Font font );
	}
}