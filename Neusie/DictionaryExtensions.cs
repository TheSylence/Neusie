using System.Collections.Generic;

namespace Neusie
{
	internal static class DictionaryExtensions
	{
		internal static void Merge( this Dictionary<string, int> dict, Dictionary<string, int> other )
		{
			foreach( var kvp in other )
			{
				if( dict.ContainsKey( kvp.Key ) )
				{
					dict[kvp.Key] += kvp.Value;
				}
				else
				{
					dict.Add( kvp.Key, kvp.Value );
				}
			}
		}
	}
}