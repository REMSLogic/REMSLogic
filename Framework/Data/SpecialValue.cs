using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data
{
	public class SpecialValue
	{
		public static SpecialValue IsNull = new SpecialValue( "is-null" );
		public static SpecialValue IsNotNull = new SpecialValue( "is-not-null" );

		public string Name;

		public SpecialValue(string name)
		{
			this.Name = name;
		}
	}
}
