using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Framework.Security
{
	public static class Hash
	{
		public static Encoding DefaultEncoding
		{
			get
			{
				var enc = Encoding.GetEncoding( Config.Manager.Framework.Hash.DefaultEncoding );

				if( enc == null )
					enc = Encoding.UTF8;

				return enc;
			}
		}

		public static string OutputType
		{
			get
			{
				var ret = Config.Manager.Framework.Hash.OutputType;

				if( string.IsNullOrWhiteSpace( ret ) )
					ret = "base64";

				if( ret != "base64" && ret != "hex" )
					throw new System.Configuration.ConfigurationErrorsException( "Invalid configuration setting. framework\\hash\\output-type must be one of [\"base64\",\"hex\"]. Supplied value was [" + ret + "]." );

				return ret;
			}
		}

		public static string Salt
		{
			get
			{
				return Config.Manager.Framework.Hash.Salt;
			}
		}

		public static string GetHash(string text, string type)
		{
			return GetHash( text, type, DefaultEncoding );
		}

		public static string GetHash(string text, string type, Encoding enc, string outputType = null)
		{
			type = type.ToLower();

			byte[] message = enc.GetBytes( text );
			HashAlgorithm algo = null;

			switch (type)
			{
			case "md5":
				algo = new MD5CryptoServiceProvider();
				break;
			case "sha1":
				algo = new SHA1Managed();
				break;
			case "sha256":
				algo = new SHA256Managed();
				break;
			case "sha384":
				algo = new SHA384Managed();
				break;
			case "sha512":
				algo = new SHA512Managed();
				break;
			default:
				throw new ArgumentException("Type must be one of ['md5', 'sha1', 'sha256', 'sha384', 'sha512'].", "type");
			}

			return GetOutput( algo.ComputeHash( message ), outputType );
		}

		public static string GetMD5(string text, string outputType = null)
		{
			return GetMD5( text, DefaultEncoding, outputType );
		}

		public static string GetMD5(string text, Encoding enc, string outputType = null)
		{
			return GetHash(text, "md5", enc, outputType);
		}

		public static string GetSHA1(string text, string outputType = null)
		{
			return GetSHA1( text, DefaultEncoding, outputType );
		}

		public static string GetSHA1(string text, Encoding enc, string outputType = null)
		{
			return GetHash(text, "sha1", enc, outputType);
		}

		public static string GetSHA256(string text, string outputType = null)
		{
			return GetSHA256( text, DefaultEncoding, outputType );
		}

		public static string GetSHA256(string text, Encoding enc, string outputType = null)
		{
			return GetHash(text, "sha256", enc, outputType);
		}

		public static string GetSHA512(string text, string outputType = null)
		{
			return GetSHA512( text, DefaultEncoding, outputType );
		}

		public static string GetSHA512(string text, Encoding enc, string outputType = null)
		{
			return GetHash(text, "sha512", enc, outputType);
		}

		private static string GetOutput(byte[] bytes)
		{
			return GetOutput(bytes, OutputType);
		}

		private static string GetOutput(byte[] bytes, string type)
		{
			if( string.IsNullOrWhiteSpace( type ) )
				type = OutputType;

			if( type != "base64" && type != "hex" )
				throw new System.Configuration.ConfigurationErrorsException( "Invalid configuration setting. framework\\hash\\output-type must be one of ['base64','hex']. Supplied value was [" + type + "]." );

			if( type == "base64" )
			{
				return Convert.ToBase64String(bytes);
			}
			else
			{
				string ret = "";

				foreach( byte x in bytes )
				{
					ret += String.Format( "{0:X2}", x );
				}

				return ret;
			}
		}
	}
}
