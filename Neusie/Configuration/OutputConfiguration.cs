using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class OutputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public OutputConfiguration( IConfigurationSection section ) : base( section )
		{
			Csv = new CsvOutputConfiguration( section.GetSection( ConfigurationKeys.CsvOutputSection ) );
			Image = new ImageOutputConfiguration( section.GetSection( ConfigurationKeys.ImageOutputSection ) );
		}

		public CsvOutputConfiguration Csv { get; }
		public ImageOutputConfiguration Image { get; }

		public int Seed => ReadInt( ConfigurationKeys.Output.Seed );
		public string TargetName => ReadString( ConfigurationKeys.Output.TargetName );
		public string TargetPath => ReadString( ConfigurationKeys.Output.TargetPath );
	}
}