using System.Collections.Generic;

namespace Neusie.TextProcessing
{
	internal interface ITextPreProcessor
	{
		string Process( string input );
	}

	internal interface ITextPostProcessor
	{
		Dictionary<string, int> Process( Dictionary<string, int> result );
	}
}