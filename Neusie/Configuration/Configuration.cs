using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class Configuration
	{
		public Configuration( IConfiguration config )
		{
			Input = new InputConfiguration( config.GetSection( ConfigurationKeys.InputSection ) );
			Output = new OutputConfiguration(config.GetSection(ConfigurationKeys.OutputSection));
		}

		public InputConfiguration Input { get; }
		public OutputConfiguration Output { get; }
	}
}