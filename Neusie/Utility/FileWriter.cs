using System;
using System.IO;

namespace Neusie.Utility
{
	internal class FileWriter : IFileWriter
	{
		/// <inheritdoc />
		public void Write( string fileName, string content )
		{
			if( string.IsNullOrEmpty( fileName ) )
			{
				throw new ArgumentNullException( nameof(fileName) );
			}

			File.WriteAllText( fileName, content );
		}
	}
}