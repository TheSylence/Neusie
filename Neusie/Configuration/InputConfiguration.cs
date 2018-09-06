using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class InputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public InputConfiguration( IConfigurationSection section ) : base( section )
		{
		}

		public IReadOnlyCollection<string> Blacklist
		{
			get
			{
				var list = new List<string>( ReadStringList( ConfigurationKeys.Input.Blacklist ) );

				if( HasKey( ConfigurationKeys.Input.BlacklistFile ) )
				{
					var fileName = ReadString( ConfigurationKeys.Input.BlacklistFile );
					list.AddRange( File.ReadAllLines( fileName ) );
				}

				return list;
			}
		}

		public bool KeepComments => ReadBool( ConfigurationKeys.Input.KeepComments );
		public bool KeepNamespaces => ReadBool( ConfigurationKeys.Input.KeepNamespaces );
		public bool KeepStrings => ReadBool( ConfigurationKeys.Input.KeepStrings );
		public int MinWordLength => ReadInt( ConfigurationKeys.Input.MinWordLength );

		public IReadOnlyCollection<string> Sources
		{
			get
			{
				if( TryReadStringList( ConfigurationKeys.Input.Sources, out var list ) )
				{
					return list;
				}

				return new string[0];
			}
		}
	}
}