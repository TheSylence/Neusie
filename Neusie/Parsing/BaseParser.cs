using System;
using System.Collections.Generic;
using System.Linq;

namespace Neusie.Parsing
{
	internal abstract class BaseParser
	{
		protected BaseParser()
		{
			AllowedExtensions = new List<string>( new[] {".cs"} );
			ForbiddenExtensions = new List<string>( new[] {".xaml.cs", ".g.cs", ".g.i.cs"} );
		}

		protected bool IsCSharpSourceFile( string fileName )
		{
			var hasAllowedExtension = AllowedExtensions.Any( e => fileName.EndsWith( e, StringComparison.Ordinal ) );
			if( !hasAllowedExtension )
			{
				return false;
			}

			var hasForbiddednExtension = ForbiddenExtensions.Any( e => fileName.EndsWith( e, StringComparison.Ordinal ) );
			return !hasForbiddednExtension;
		}

		private readonly List<string> AllowedExtensions;
		private readonly List<string> ForbiddenExtensions;
	}
}