using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusie.Generation
{
	internal class CsvGenerator : IGenerator
	{
		public IData Generate( Dictionary<string, int> words )
		{
			var sb = new StringBuilder();

			sb.AppendLine( "Word;Count" );
			foreach( var kvp in words.OrderByDescending( x => x.Value ) )
			{
				sb.AppendLine( $"{kvp.Key};{kvp.Value}" );
			}

			return new StringData( sb.ToString(), ".csv" );
		}
	}
}