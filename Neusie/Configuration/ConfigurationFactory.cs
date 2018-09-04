using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal static class ConfigurationFactory
	{
		public static Configuration Build( string[] args )
		{
			return new Configuration( CreateRoot( args, false ) );
		}

		internal static IConfigurationRoot CreateRoot( string[] args, bool isConfigFileOptional )
		{
			var builder = new ConfigurationBuilder()
				.AddInMemoryCollection( GenerateDefaultConfig() )
				.AddCommandLine( args, CreateCommandLineSwitchMap() );

			var configFile = FindConfigFile( args );
			if( configFile != null )
			{
				builder.AddJsonFile( configFile, isConfigFileOptional );
			}

			return builder.Build();
		}

		private static IDictionary<string, string> CreateCommandLineSwitchMap()
		{
			return new Dictionary<string, string>
			{
				{"-i", "input:sources"},
				{"--input", "input:sources"}
			};
		}

		private static string FindConfigFile( string[] args )
		{
			var tmpConfig = new ConfigurationBuilder().AddCommandLine( args ).Build();

			return tmpConfig["config"];
		}

		private static IEnumerable<KeyValuePair<string, string>> GenerateDefaultConfig()
		{
			foreach( var keyValuePair in GenerateDefaultInputConfig() )
			{
				yield return keyValuePair;
			}

			foreach( var keyValuePair in GenerateDefaultOutputConfig() )
			{
				yield return keyValuePair;
			}
		}

		private static IEnumerable<KeyValuePair<string, string>> GenerateDefaultInputConfig()
		{
			yield return new KeyValuePair<string, string>( KeyName.SuffixWithCounter( "input:blacklist", 0 ), "microsoft" );
			yield return new KeyValuePair<string, string>( KeyName.SuffixWithCounter( "input:blacklist", 1 ), "system" );
			yield return new KeyValuePair<string, string>( KeyName.SuffixWithCounter( "input:blacklist", 2 ), "var" );
			yield return new KeyValuePair<string, string>( "input:minwordlength", "2" );
		}

		private static IEnumerable<KeyValuePair<string, string>> GenerateDefaultOutputConfig()
		{
			yield return new KeyValuePair<string, string>( "output:targetpath", Directory.GetCurrentDirectory() );
			yield return new KeyValuePair<string, string>( "output:targetname", "noisemap" );
		}
	}
}