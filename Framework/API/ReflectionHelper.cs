using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.API
{
	static class ReflectionHelper
	{
		public static System.Reflection.MethodInfo FindMethod(Type t, string name)
		{
			var methods = t.GetMethods();
			foreach( var method in methods )
			{
				if( method.Name == name )
					return method;
			}

			return null;
		}

		public static System.Reflection.MethodInfo FindMethod(Type t, string name, Type returnType, Type[] signature)
		{
			var methods = t.GetMethods();
			foreach( var method in methods )
			{
				if( method.Name == name && method.ReturnType == returnType )
				{
					var ps = method.GetParameters();
					if( ps.Length != signature.Length )
						continue;

					var match = true;

					for( int i = 0; i < ps.Length; i++ )
						if( ps[i].ParameterType != signature[i] )
							match = false;

					if( match )
						return method;
				}
			}

			return null;
		}
	}
}
