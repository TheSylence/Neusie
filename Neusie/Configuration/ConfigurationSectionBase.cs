using System;
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

		protected bool HasKey( string key )
		{
			return !string.IsNullOrEmpty( Section[key] );
		}

		protected bool ReadBool( string key )
		{
			var value = Section[key];
			if( string.IsNullOrEmpty( value ) )
			{
				throw new KeyNotFoundException( key );
			}

			if( !bool.TryParse( value, out var result ) )
			{
				throw new FormatException();
			}

			return result;
		}

		protected int ReadInt( string key )
		{
			var value = Section[key];
			if( string.IsNullOrEmpty( value ) )
			{
				throw new KeyNotFoundException( key );
			}

			if( !int.TryParse( value, NumberStyles.Any, CultureInfo.InvariantCulture, out var number ) )
			{
				throw new FormatException();
			}

			return number;
		}

		protected string ReadString( string key )
		{
			var value = Section[key];
			if( string.IsNullOrEmpty( value ) )
			{
				throw new KeyNotFoundException( key );
			}

			return value;
		}

		protected IReadOnlyCollection<string> ReadStringList( string key )
		{
			var list = ReadStringList( key, out var notFound );
			if( notFound )
			{
				throw new KeyNotFoundException();
			}

			return list;
		}

		protected bool TryReadStringList( string key, out IReadOnlyCollection<string> list )
		{
			list = ReadStringList( key, out var notFound );
			if( notFound )
			{
				return false;
			}

			return true;
		}

		private IReadOnlyCollection<string> ReadStringList( string key, out bool notFound )
		{
			var counter = 0;

			var hasKey = HasKey( KeyName.SuffixWithCounter( key, counter ) );
			if( !hasKey )
			{
				if( !HasKey( key ) )
				{
					notFound = true;
					return new string[0];
				}

				var item = ReadString( key );
				notFound = false;
				return new List<string>( item?.Yield() ?? Enumerable.Empty<string>() );
			}

			var list = new List<string>();
			while( hasKey )
			{
				list.Add( ReadString( KeyName.SuffixWithCounter( key, counter ) ) );

				++counter;
				hasKey = HasKey( KeyName.SuffixWithCounter( key, counter ) );
			}

			notFound = false;
			return list;
		}

		private readonly IConfigurationSection Section;
	}
}