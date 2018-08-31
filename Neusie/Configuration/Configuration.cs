using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class Configuration
	{
		public Configuration( IConfiguration config )
		{
			Input = new InputConfiguration( config.GetSection( ConfigurationKeys.InputSection ) );
		}

		public InputConfiguration Input { get; }
	}
}