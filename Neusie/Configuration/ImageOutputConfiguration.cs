using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class ImageOutputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public ImageOutputConfiguration( IConfigurationSection section ) : base( section )
		{
		}

		public int Compactness => ReadInt( ConfigurationKeys.ImageOutput.Compactness );
		public string Font => ReadString( ConfigurationKeys.ImageOutput.Font );
		public int Height => ReadInt( ConfigurationKeys.ImageOutput.Height );
		public bool IsEnabled => ReadBool( ConfigurationKeys.ImageOutput.Enabled );
		public int MinimumFontSize => ReadInt( ConfigurationKeys.ImageOutput.MinimumFontSize );
		public int Width => ReadInt( ConfigurationKeys.ImageOutput.Width );
	}
}