using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Framework.API
{
	public class Base
	{
		protected static bool IsWebSafe(string value)
		{
			var r = new Regex( @"[^A-Za-z0-9_\-]+" );
			return !(r.IsMatch( value ));
		}
	}
}
