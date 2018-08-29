using System.IO;

namespace Neusie.Generation.Csv
{
	internal class CsvData : BaseData
	{
		/// <inheritdoc />
		public CsvData( string content, string extension ) : base( extension )
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