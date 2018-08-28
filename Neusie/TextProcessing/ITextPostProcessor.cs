using System.Collections.Generic;

namespace Neusie.TextProcessing
{
	internal interface ITextPostProcessor
	{
		Dictionary<string, int> Process( Dictionary<string, int> result );
	}
}