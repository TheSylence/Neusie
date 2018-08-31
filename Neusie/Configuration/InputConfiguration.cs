using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class InputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public InputConfiguration( IConfigurationSection section ) : base( section )
		{
		}

		public IReadOnlyCollection<string> Sources => ReadStringList( ConfigurationKeys.Input.Sources );
	}
}