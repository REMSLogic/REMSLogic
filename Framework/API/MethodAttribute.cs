using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.API
{
	public class MethodAttribute : Attribute
	{
		public string Path
		{ get; protected set; }

		public MethodAttribute(string Path)
		{
			this.Path = Path;
		}
	}
}
