using System.Collections.Generic;

namespace Neusie.Utility
{
	internal static class ObjectExtensions
	{
		internal static IEnumerable<T> Yield<T>( this T item )
		{
			return new[] {item};
		}
	}
}