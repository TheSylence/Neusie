using System.Collections.Generic;
using System.Globalization;
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

		protected bool? ReadBool( string key )
		{
			var value = Section[key];
			if( string.IsNullOrEmpty( value ) )
			{
				return null;
			}

			if( !bool.TryParse( value, out var result ) )
			{
				return null;
			}

			return result;
		}

		protected int? ReadInt( string key )
		{
			var value = Section[key];
			if( string.IsNullOrEmpty( value ) )
			{
				return null;
			}

			if( !int.TryParse( value, NumberStyles.Any, CultureInfo.InvariantCulture, out var number ) )
			{
				return null;
			}

			return number;
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

			var counterElement = ReadString( KeyName.SuffixWithCounter( key, counter ) );
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
				counterElement = ReadString( KeyName.SuffixWithCounter( key, counter ) );
			}

			return list;
		}

		private readonly IConfigurationSection Section;
	}
}