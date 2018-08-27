using System.Collections.Generic;

namespace Neusie.Generation
{
	internal interface IGenerator
	{
		IData Generate( Dictionary<string, int> words );
	}
}