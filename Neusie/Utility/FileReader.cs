using System;
using System.IO;

namespace Neusie.Utility
{
	internal class FileReader : IFileReader
	{
		public string Read( string fileName )
		{
			if( fileName == null )
			{
				throw new ArgumentNullException( nameof(fileName) );
			}

			return File.Exists( fileName )
				? File.ReadAllText( fileName )
				: null;
		}
	}
}