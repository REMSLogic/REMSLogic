using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.API
{
	class TypeConversion
	{
		protected delegate object ConvertMethod(object input, out bool error);

		protected static Dictionary<Type, ConvertMethod> convMethods;
		static TypeConversion()
		{
			convMethods = new Dictionary<Type, ConvertMethod>();

			convMethods.Add( typeof( String ), ConvertString );
			convMethods.Add( typeof( Byte ), ConvertByte );
			convMethods.Add( typeof( Int16 ), ConvertInt16 );
			convMethods.Add( typeof( UInt16 ), ConvertUInt16 );
			convMethods.Add( typeof( Int32 ), ConvertInt32 );
			convMethods.Add( typeof( UInt32 ), ConvertUInt32 );
			convMethods.Add( typeof( Int64 ), ConvertInt64 );
			convMethods.Add( typeof( UInt64 ), ConvertUInt64 );
			convMethods.Add( typeof( Nullable<Int64> ), ConvertNullableInt64 );
			convMethods.Add( typeof( Single ), ConvertSingle );
			convMethods.Add( typeof( Double ), ConvertDouble );
			convMethods.Add( typeof( DateTime ), ConvertDateTime );
			convMethods.Add( typeof( Nullable<DateTime> ), ConvertNullableDateTime );
			convMethods.Add( typeof( Boolean ), ConvertBoolean );
			convMethods.Add( typeof( Int64[] ), ConvertInt64Array );
            convMethods.Add( typeof( List<string> ), ConvertList );
            // MJL 2013-11-29 - The data comes in as ann array, not a list
			// TJM 2013-12-28 - This class returns what the api function expects, not what is passed in from the client, fix and remove this!
            convMethods.Add( typeof( string[] ), ConvertStringArray );
            convMethods.Add( typeof(System.Web.HttpFileCollection), ConvertFileCollection );
		}

		public static bool CanConvert(Type t)
		{
			return convMethods.ContainsKey( t );
		}

		public static object Convert(Type t, object input, out bool error)
		{
			if( !convMethods.ContainsKey( t ) )
			{
				error = true;
				return null;
			}

			return convMethods[t](input, out error);
		}

		private static object ConvertString(object input, out bool error)
		{
			error = false;
			return input;
		}

        private static object ConvertFileCollection(object input, out bool error)
        {
            error = false;
            return input;
        }

		private static object ConvertByte(object input, out bool error)
		{
			Byte ret;
			if( !Byte.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertInt16(object input, out bool error)
		{
			Int16 ret;
			if( !Int16.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertUInt16(object input, out bool error)
		{
			UInt16 ret;
			if( !UInt16.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertInt32(object input, out bool error)
		{
			Int32 ret;
			if( !Int32.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertUInt32(object input, out bool error)
		{
			UInt32 ret;
			if( !UInt32.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertInt64(object input, out bool error)
		{
			Int64 ret;
			if( !Int64.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertUInt64(object input, out bool error)
		{
			UInt64 ret;
			if( !UInt64.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertNullableInt64(object input, out bool error)
		{
			Int64 ret;
			if( !Int64.TryParse( (string)input, out ret ) )
			{
				error = false;
				return new Nullable<Int64>();
			}

			error = false;
			return new Nullable<Int64>( ret );
		}

		private static object ConvertSingle(object input, out bool error)
		{
			Single ret;
			if( !Single.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertDouble(object input, out bool error)
		{
			Double ret;
			if( !Double.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertDateTime(object input, out bool error)
		{
			DateTime ret;
			if( !DateTime.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertNullableDateTime(object input, out bool error)
		{
			DateTime ret;
			if( !DateTime.TryParse( (string)input, out ret ) )
			{
				error = false;
				return new Nullable<DateTime>();
			}

			error = false;
			return new Nullable<DateTime>( ret );
		}

		private static object ConvertBoolean(object input, out bool error)
		{
			Boolean ret;
			if( !Boolean.TryParse( (string)input, out ret ) )
			{
				error = true;
				return null;
			}

			error = false;
			return ret;
		}

		private static object ConvertInt64Array(object input, out bool error)
		{
			var inArr = (string[])input;
			Int64 val;
			long[] ret = new long[inArr.Length];

			for( int i = 0; i < inArr.Length; i++ )
			{
				if( !Int64.TryParse( inArr[i], out val ) )
				{
					error = true;
					return null;
				}

				ret[i] = val;
			}
			
			error = false;
			return ret;
		}

        private static object ConvertList(object input, out bool error)
        {
            var inArr = (string[])input;
            error = false;
            return inArr;
        }

        // MJL 2013-11-29 - The data comes in as ann array, not a list
        private static object ConvertStringArray(object input, out bool error)
        {
            var inArr = (string[])input;
            error = false;
            return inArr;
        }
	}
}
