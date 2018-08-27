using System;
using System.IO;

namespace Neusie.TextProcessing
{
	internal class FileReader
	{
		public string Read( string fileName )
		{
			if( fileName == null )
			{
				throw new ArgumentNullException( nameof(fileName) );
			}

			if( !File.Exists( fileName ) )
			{
				throw new FileNotFoundException();
			}

			return File.ReadAllText( fileName );
		}
	}
}