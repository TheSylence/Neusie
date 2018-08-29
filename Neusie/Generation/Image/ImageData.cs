using System.Drawing;
using System.Drawing.Imaging;

namespace Neusie.Generation.Image
{
	internal class ImageData : BaseData
	{
		/// <inheritdoc />
		public ImageData( Bitmap bitmap ) : base( ".png" )
		{
			Bitmap = bitmap;
		}

		/// <inheritdoc />
		public override void Save( string baseName )
		{
			var fileName = FileName( baseName );

			Bitmap.Save( fileName, ImageFormat.Png );
		}

		private readonly Bitmap Bitmap;
	}
}