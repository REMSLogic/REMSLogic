using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.CodeGen
{
	public class Class
	{
		public string Name;
		public string Inherits;
		public List<Method> StaticMethods;
		public List<Property> Properties;
		public List<Field> Fields;
		public List<Method> Constructors;
		public List<Method> Methods;
	}
}
