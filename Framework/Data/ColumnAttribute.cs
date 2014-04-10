using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data
{
	public class ColumnAttribute : Attribute
	{
		public string Name;

		public ColumnAttribute()
		{
			Name = null;
		}

		public ColumnAttribute(string name)
		{
			Name = name;
		}
	}
}
