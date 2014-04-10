using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.CodeGen
{
	public class Field
	{
		public List<Framework.CodeGen.Attribute> Attributes;
		public Accessor Accessor;
		public bool IsStatic;
		public string Type;
		public string Name;
	}
}
