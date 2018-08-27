using System.IO;

namespace Neusie.Generation
{
	internal abstract class Data : IData
	{
		protected Data( string extension )
		{
			Extension = extension;
		}

		protected string FileName( string baseName )
		{
			return Path.ChangeExtension( baseName, Extension );
		}

		public abstract void Save( string baseName );

		private readonly string Extension;
	}
}