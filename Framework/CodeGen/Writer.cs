using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Framework.CodeGen
{
	public static class Writer
	{
		public static void Write(string file_name, File file)
		{
			var sb = new StringBuilder();

			foreach(var u in file.Usings)
			{
				sb.AppendLine("using "+u+";");
			}

			sb.AppendLine();

			var ns = file.Namespace;
			sb.AppendLine("namespace "+ns.Name);
			sb.AppendLine("{");

			var c = ns.Class;
			sb.AppendLine("\tpublic class " + c.Name + (string.IsNullOrWhiteSpace(c.Inherits) ? "" : " : " + c.Inherits));
			sb.AppendLine("\t{");

			if (c.StaticMethods != null && c.StaticMethods.Count > 0)
			{
				foreach( var m in c.StaticMethods )
					WriteMethod(ref sb, m);

				sb.AppendLine("\t\t");
			}

			if (c.Properties != null && c.Properties.Count > 0)
			{
				foreach (var p in c.Properties)
				{
					WriteAttributes(ref sb, p.Attributes);

					sb.Append("\t\t" + Accessor(p.Accessor) + " ");

					if (p.IsStatic)
						sb.Append("static ");

					sb.AppendLine(p.Type + " " + p.Name);
					sb.AppendLine("\t\t{");

					if( !string.IsNullOrWhiteSpace(p.GetBody) )
					{
						sb.AppendLine("\t\t\tget");
						sb.AppendLine("\t\t\t{");

						var lines = p.GetBody.Split('\n');

						foreach (var line in lines)
							sb.AppendLine("\t\t\t\t" + line);

						sb.AppendLine("\t\t\t}");
					}
					if( !string.IsNullOrWhiteSpace(p.SetBody) )
					{
						sb.AppendLine("\t\t\tset");
						sb.AppendLine("\t\t\t{");

						var lines = p.SetBody.Split('\n');

						foreach (var line in lines)
							sb.AppendLine("\t\t\t\t" + line);

						sb.AppendLine("\t\t\t}");
					}

					sb.AppendLine("\t\t}");
				}

				sb.AppendLine("\t\t");
			}

			if (c.Fields != null && c.Fields.Count > 0)
			{
				foreach (var f in c.Fields)
				{
					WriteAttributes(ref sb, f.Attributes);

					sb.Append("\t\t" + Accessor(f.Accessor) + " ");
					
					if( f.IsStatic )
						sb.Append("static ");

					sb.AppendLine(f.Type+" "+f.Name+";");
				}

				sb.AppendLine("\t\t");
			}

			if (c.Constructors != null && c.Constructors.Count > 0)
			{
				foreach (var con in c.Constructors)
					WriteMethod(ref sb, con);

				sb.AppendLine("\t\t");
			}

			if (c.Methods != null && c.Methods.Count > 0)
			{
				foreach (var m in c.Methods)
					WriteMethod(ref sb, m);
			}

			sb.AppendLine("\t}");

			sb.AppendLine("}");

			var sw = new StreamWriter(file_name);
			sw.Write(sb.ToString());
			sw.Close();
		}

		private static void WriteMethod(ref StringBuilder sb, Method m)
		{
			WriteAttributes(ref sb, m.Attributes);

			sb.Append("\t\t"+Accessor(m.Accessor)+" ");

			if (m.IsStatic)
				sb.Append("static ");
			if (m.IsOverride)
				sb.Append("override ");
			if (m.IsVirtual)
				sb.Append("virtual ");

			if( !string.IsNullOrWhiteSpace(m.ReturnType) )
				sb.Append(m.ReturnType+" ");
			
			sb.Append(m.Name+"(");

			if( m.Parameters != null )
			{
				bool first = true;
				foreach( var p in m.Parameters )
				{
					if( !first )
						sb.Append(", ");

					first = false;
					sb.Append(p.Type+" "+p.Name+(!string.IsNullOrWhiteSpace(p.DefaultValue) ? " = "+p.DefaultValue : ""));
				}
			}

			sb.AppendLine(")");

			if( !string.IsNullOrWhiteSpace(m.InitializerList) )
				sb.AppendLine("\t\t\t: " + m.InitializerList);

			sb.AppendLine("\t\t{");

			if( !string.IsNullOrWhiteSpace(m.Body) )
			{
				var lines = m.Body.Split('\n');

				foreach( var line in lines )
					sb.AppendLine("\t\t\t" + line);
			}

			sb.AppendLine("\t\t}");
		}

		private static void WriteAttributes(ref StringBuilder sb, List<Framework.CodeGen.Attribute> attrs)
		{
			if( attrs == null || attrs.Count <= 0 )
				return;

			foreach (var a in attrs)
			{
				sb.Append("\t\t[" + a.Name);

				if (a.Parameters != null)
				{
					sb.Append("(");
					bool first = true;
					foreach (var p in a.Parameters)
					{
						if (!first)
							sb.Append(", ");

						first = false;
						sb.Append(p);
					}
					sb.Append(")");
				}

				sb.AppendLine("]");
			}
		}

		private static string Accessor(Accessor a)
		{
			switch(a)
			{
			case CodeGen.Accessor.Private:
				return "private";
			case CodeGen.Accessor.Internal:
				return "internal";
			case CodeGen.Accessor.Protected:
				return "protected";
			case CodeGen.Accessor.Public:
			default:
				return "public";
			}
		}
	}
}
