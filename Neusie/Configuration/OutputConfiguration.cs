using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	class OutputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public OutputConfiguration( IConfigurationSection section ) : base( section )
		{
		}

		public string TargetPath => ReadString(ConfigurationKeys.Output.TargetPath);
	}
}