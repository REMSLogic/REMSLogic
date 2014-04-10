using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data
{
	public class Command
	{
		public static string HandleParameter( string name, object value, Database db, ref List<Parameter> ps )
		{
			string ret = db.DelimColumn( name );
			if( value != null && value.GetType() == typeof(SpecialValue) )
			{
				// Special Handling
				var sv = (SpecialValue)value;

				switch( sv.Name )
				{
				case "is-null":
					ret += " IS NULL";
					return ret;
				case "is-not-null":
					ret += " IS NOT NULL";
					return ret;
				case "lit-value":
					var lv = (LiteralValue)sv;
					ret += " = " + lv.TextValue;
					return ret;
				default:
					throw new ArgumentOutOfRangeException( "value", "Unhandled Special Value passed in." );
				}
			}

			var pname = "auto_"+name+"_"+ps.Count.ToString();
			ret += " = " + db.DelimParameter( pname );

			ps.Add( new Parameter( pname, value ) );

			return ret;
		}

		public static string ParseOrderBy( Database db, IEnumerable<string> order_by )
		{
			if( order_by == null )
				return "";

			var ret = "ORDER BY ";
			bool first = true;

			foreach( var o in order_by )
			{
				if( string.IsNullOrWhiteSpace( o ) )
					continue;

				if( !first )
					ret += ", ";

				first = false;

				var dir = "ASC";
				var name = o;

				if( o[0] == '+' )
				{
					name = o.Substring( 1 );
				}
				else if( o[0] == '-' )
				{
					dir = "DESC";
					name = o.Substring( 1 );
				}

				ret += db.DelimColumn( name ) + " " + dir;
			}

			if( first == true ) // Nothing was added
				return "";

			return ret;
		}
	}
}
