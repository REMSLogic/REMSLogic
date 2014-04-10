using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data
{
	public class LiteralValue : SpecialValue
	{
		public string TextValue;

		public LiteralValue( string txt ) : base("lit-value")
		{
			TextValue = txt;
		}
	}
}
