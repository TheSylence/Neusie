using Microsoft.Extensions.Configuration;

namespace Neusie.Configuration
{
	internal class ImageOutputConfiguration : ConfigurationSectionBase
	{
		/// <inheritdoc />
		public ImageOutputConfiguration( IConfigurationSection section ) : base( section )
		{
		}

		private const string DefaultFont = "Tahoma";
		private const int DefaultHeight = 1024;
		private const int DefaultWidth = 1024;

		public string Font => ReadString( ConfigurationKeys.ImageOutput.Font ) ?? DefaultFont;
		public int Height => ReadInt( ConfigurationKeys.ImageOutput.Height ) ?? DefaultHeight;
		public bool IsEnabled => ReadBool( ConfigurationKeys.ImageOutput.Enabled ) ?? true;
		public int Width => ReadInt( ConfigurationKeys.ImageOutput.Width ) ?? DefaultWidth;
	}
}