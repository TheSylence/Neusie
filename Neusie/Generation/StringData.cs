using System.IO;

namespace Neusie.Generation
{
	internal class StringData : Data
	{
		/// <inheritdoc />
		public StringData( string content, string extension ) : base( extension )
		{
			Content = content;
		}

		/// <inheritdoc />
		public override void Save( string baseName )
		{
			var fileName = FileName( baseName );

			File.WriteAllText( fileName, Content );
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return Content;
		}

		private readonly string Content;
	}
}