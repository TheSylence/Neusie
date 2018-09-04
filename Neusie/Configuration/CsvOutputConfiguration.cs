using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	class CsvOutputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public CsvOutputConfiguration( IConfigurationSection section ) : base( section )
		{
		}

		public bool IsEnabled => ReadBool(ConfigurationKeys.CsvOutput.Enabled) ?? true;
	}
}