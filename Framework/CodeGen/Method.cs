using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.CodeGen
{
	public class Method
	{
		public List<Framework.CodeGen.Attribute> Attributes;
		public Accessor Accessor;
		public bool IsStatic;
		public bool IsOverride;
		public bool IsVirtual;
		public string ReturnType;
		public string Name;
		public List<Parameter> Parameters;
		public string InitializerList;
		public string Body;
	}
}
