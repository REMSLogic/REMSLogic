using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
	public class Encryption
	{
		public static string Encrypt(string value)
		{
			byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes( value );
			byte[] keyArray = Convert.FromBase64String( Config.Manager.Framework.Encryption.Key );
			byte[] ivArray = Convert.FromBase64String(Config.Manager.Framework.Encryption.IV );
			
			TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
			tDes.Key = keyArray;
			tDes.Mode = CipherMode.CBC;
			tDes.Padding = PaddingMode.PKCS7;
			tDes.IV = ivArray;

			ICryptoTransform cTransform = tDes.CreateEncryptor();
			byte[] resultArray = cTransform.TransformFinalBlock( toEncryptArray, 0, toEncryptArray.Length );
			tDes.Clear();
			return Convert.ToBase64String( resultArray, 0, resultArray.Length );
		}

		public static string Decrypt(string value)
		{
			byte[] toDecryptArray = Convert.FromBase64String( value );
			byte[] keyArray = Convert.FromBase64String( Config.Manager.Framework.Encryption.Key );
			byte[] ivArray = Convert.FromBase64String(Config.Manager.Framework.Encryption.IV);

			TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
			tDes.Key = keyArray;
			tDes.Mode = CipherMode.CBC;
			tDes.Padding = PaddingMode.PKCS7;
			tDes.IV = ivArray;

			ICryptoTransform cTransform = tDes.CreateDecryptor();
			try
			{
				byte[] resultArray = cTransform.TransformFinalBlock( toDecryptArray, 0, toDecryptArray.Length );

				tDes.Clear();
				return UTF8Encoding.UTF8.GetString( resultArray, 0, resultArray.Length );
			}
			catch( Exception ex )
			{
				throw ex;
			}
		}
	}
}
