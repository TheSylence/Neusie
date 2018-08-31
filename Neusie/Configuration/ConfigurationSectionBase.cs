using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Neusie.Utility;

namespace Neusie.Configuration
{
	internal abstract class ConfigurationSectionBase
	{
		protected ConfigurationSectionBase( IConfigurationSection section )
		{
			Section = section;
		}

		protected string ReadString( string key )
		{
			var value = Section[key];
			if( string.IsNullOrEmpty( value ) )
			{
				return null;
			}

			return value;
		}

		protected IReadOnlyCollection<string> ReadStringList( string key )
		{
			var counter = 0;

			var counterElement = ReadString( KeyName( key, counter ) );
			if( counterElement == null )
			{
				var item = ReadString( key );
				return new List<string>( item?.Yield() ?? Enumerable.Empty<string>() );
			}

			var list = new List<string>();
			while( counterElement != null )
			{
				list.Add( counterElement );

				++counter;
				counterElement = ReadString( KeyName( key, counter ) );
			}

			return list;
		}

		private string KeyName( string key, int counter )
		{
			return $"{key}:{counter}";
		}

		private readonly IConfigurationSection Section;
	}
}