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

				var fileName = ReadString( ConfigurationKeys.Input.BlacklistFile );
				if( fileName != null )
				{
					list.AddRange( File.ReadAllLines( fileName ) );
				}

				return list;
			}
		}

		public bool KeepComments => ReadBool( ConfigurationKeys.Input.KeepComments ) ?? false;
		public bool KeepNamespaces => ReadBool( ConfigurationKeys.Input.KeepNamespaces ) ?? false;
		public bool KeepStrings => ReadBool( ConfigurationKeys.Input.KeepStrings ) ?? false;
		public int MinWordLength => ReadInt( ConfigurationKeys.Input.MinWordLength ) ?? 0;

		public IReadOnlyCollection<string> Sources => ReadStringList( ConfigurationKeys.Input.Sources );
	}
}