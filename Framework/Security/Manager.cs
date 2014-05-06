using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Framework.Security
{
	public static class Manager
	{
		public static User GetUser()
		{
			if( HttpContext.Current.Session["User"] == null )
				return null;

			return HttpContext.Current.Session["User"] as User;
		}

		public static User CreateUser(string username, string password, string email, out string error)
		{
			error = "";

			var u = new User();
			u.Username = username;
			u.Email = email;
			u.ResetPasswordGuid = Guid.Empty;
			u.LastLogin = DateTime.Now;

			string salt = GetRandomSalt();
			u.PasswordSalt = salt;
			u.Password = Hash.GetHash( password + salt, Config.Manager.Framework.Security.Authentication.HashingMethod ?? "sha512", Encoding.UTF8, "hex" );

			u.Save();

			return u;
		}

		public static bool ChangePassword(User u, string currPassword, string newPassword)
		{
			if( Hash.GetHash( currPassword + u.PasswordSalt, Config.Manager.Framework.Security.Authentication.HashingMethod ?? "sha512", Encoding.UTF8, "hex" ) != u.Password )
				return false;

			u.PasswordSalt = GetRandomSalt();
			u.Password = Hash.GetHash( newPassword + u.PasswordSalt, Config.Manager.Framework.Security.Authentication.HashingMethod ?? "sha512", Encoding.UTF8, "hex" );
			u.Save();

			return true;
		}

		public static bool SetPassword(User u, string newPassword)
		{
			u.PasswordSalt = GetRandomSalt();
			u.Password = Hash.GetHash( newPassword + u.PasswordSalt, Config.Manager.Framework.Security.Authentication.HashingMethod ?? "sha512", Encoding.UTF8, "hex" );
			u.Save();

			return true;
		}

		public static bool Login(User u)
		{
			if (u == null || !u.ID.HasValue)
				return false;

			HttpContext.Current.Session["User"] = u;

			return true;
		}

		public static bool Login(string username, string password)
		{
			var db = Data.Database.Get( Config.Manager.Framework.Security.Authentication.Connection );
			string sql = "SELECT * " +
						"FROM " + db.Delim( Config.Manager.Framework.Security.Authentication.User.Table, Data.DelimType.Table ) + " " +
						"WHERE " + db.Delim( "Username", Data.DelimType.Column ) + " = @un OR " + db.Delim( "Email", Data.DelimType.Column ) + " = @e";

            var parameters = new List<Data.Parameter>();
            parameters.Add(new Data.Parameter("un", username));
            parameters.Add(new Data.Parameter("e", username));

			var rows = db.ExecuteQuery( sql, parameters.ToArray() );
			if( rows != null && rows.Count == 1 )
			{
				var row = rows[0];
				string salt = (string)row["PasswordSalt"];
				if( ((string)row["Password"]) == Hash.GetHash( password + salt, Config.Manager.Framework.Security.Authentication.HashingMethod ?? "sha512", Encoding.UTF8, "hex" ) )
				{
					var user = new User( row );
					user.LastLogin = DateTime.Now;
					user.Save();
					// Set to Session

					HttpContext.Current.Session["User"] = user;

					return true;
				}
			}

			return false;
		}

		public static bool Login()
		{
			var context = System.Web.HttpContext.Current;

			if( context == null )
				return false;

			var req = context.Request;
			var resp = context.Response;

			if( req == null || resp == null )
				return false;

			var cookie = req.Cookies["app_login"];

			if( cookie == null )
				return false;

			string str = cookie.Value;

			try
			{
				str = Encryption.Decrypt( str );
			}
			catch( Exception )
			{
				return false;
			}

			string[] parts = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries );
			if( parts == null || parts.Length != 6 )
				return false;

			var un = parts[0].Replace( "%7C", "|" );
			var ua = parts[2].Replace( "%7C", "|" );
			var hash = parts[4].Replace( "%7C", "|" );

			int r1, r2, rxor;

			if( !int.TryParse(parts[1], out r1) ||!int.TryParse(parts[3], out r2) ||!int.TryParse(parts[5], out rxor) || (r1^r2) != rxor)
				return false;

			if( req.UserAgent != ua )
				return false;

			var u = FindUser( un );

			if( string.IsNullOrEmpty( u.Username ) || u.Username != un )
				return false;

			var hash2 = Hash.GetHash( u.ID.ToString() + "|" + Hash.Salt + "|" + req.UserHostAddress, "sha512", Encoding.UTF8, "base64" );

			if( hash != hash2 )
				return false;

			u.LastLogin = DateTime.Now;
			u.Save();

			return Login( u );
		}

		public static bool GenerateLoginCookie()
		{
			var u = GetUser();

			if( u == null || u.ID == null )
				return false;

			var context = HttpContext.Current;

			if( context == null )
				return false;

			var req = context.Request;
			var resp = context.Response;

			if( req == null || resp == null )
				return false;

			var uid = u.ID.Value;
			var un = u.Username ?? u.Email;
			var ua = req.UserAgent;
			var ip = req.UserHostAddress;

			var hash = Hash.GetHash( uid.ToString() + "|" + Hash.Salt + "|" + ip, "sha512", Encoding.UTF8, "base64" );

			var r = new Random();

			var r1 = r.Next( Int32.MinValue, Int32.MaxValue );
			var r2 = r.Next( Int32.MinValue, Int32.MaxValue );

			var str = un.Replace( "|", "%7C" ) + "|" + r1.ToString() + "|" + ua.Replace( "|", "%7C" ) + "|" + r2.ToString() + "|" + hash.Replace( "|", "%7C" ) + "|" + (r1 ^ r2).ToString();

			str = Encryption.Encrypt( str );

			var cookie = new HttpCookie( "app_login" );
			cookie.Expires = DateTime.Now.AddYears( 1 );
			cookie.Value = str;

			resp.Cookies.Add( cookie );

			return true;
		}

		public static void Logout()
		{
			HttpContext.Current.Session["User"] = null;
		}

		public static bool UserExists(string email, string username)
		{
			var db = Data.Database.Get( Config.Manager.Framework.Security.Authentication.Connection );
			string sql = "SELECT * " +
						"FROM " + db.Delim( Config.Manager.Framework.Security.Authentication.User.Table, Data.DelimType.Table ) + " " +
						"WHERE " + db.Delim( "Username", Data.DelimType.Column ) + " = @un OR " + db.Delim( "Email", Data.DelimType.Column ) + " = @e";

            var parameters = new List<Framework.Data.Parameter>();
            parameters.Add(new Framework.Data.Parameter("un", username));
            parameters.Add(new Framework.Data.Parameter("e", email));

			var rows = db.ExecuteQuery( sql, parameters.ToArray() );
			if( rows != null && rows.Count == 1 )
			{
				return true;
			}

			return false;
		}

		public static bool ResetPassword(ref User u)
		{
			if (u == null || u.ID == null)
				return false;

			u.ResetPasswordGuid = Guid.NewGuid();
			u.Save();

			return true;
		}

		public static User FindUser(Guid forgotPassword)
		{
			var db = Data.Database.Get( Config.Manager.Framework.Security.Authentication.Connection );
			string sql = "SELECT * " +
						"FROM " + db.Delim( Config.Manager.Framework.Security.Authentication.User.Table, Data.DelimType.Table ) + " " +
						"WHERE " + db.Delim( "ResetPasswordGuid", Data.DelimType.Column ) + " = @fp";

            var parameters = new List<Framework.Data.Parameter>();
            parameters.Add(new Framework.Data.Parameter("fp", forgotPassword));

			var rows = db.ExecuteQuery( sql, parameters.ToArray() );
			if( rows.Count >= 1 )
				return new User( rows[0] );

			return null;
		}

		public static User FindUser( string username )
		{
			var db = Data.Database.Get( Config.Manager.Framework.Security.Authentication.Connection );
			string sql = "SELECT * " +
						"FROM " + db.Delim( Config.Manager.Framework.Security.Authentication.User.Table, Data.DelimType.Table ) + " " +
						"WHERE " + db.Delim( "Username", Data.DelimType.Column ) + " = @un OR " + db.Delim( "Email", Data.DelimType.Column ) + " = @e";

			var parameters = new List<Framework.Data.Parameter>();
			parameters.Add( new Framework.Data.Parameter( "un", username ) );
			parameters.Add( new Framework.Data.Parameter( "e", username ) );

			var rows = db.ExecuteQuery( sql, parameters.ToArray() );
			if( rows.Count == 1 )
				return new User( rows[0] );

			return null;
		}

		public static bool IsLoggedIn()
		{
			if( HttpContext.Current.Session["User"] == null )
				return false;

			User u = HttpContext.Current.Session["User"] as User;

			if( u == null || u.ID == null || u.ID <= 0 )
				return false;

			return true;
		}

		public static bool HasRole(string name, bool ignore_dev = false)
		{
			var u = GetUser();
			if( u == null )
				return false;

			if (!ignore_dev && u.IsInGroup(Group.FindByName("dev")))
				return true;

			var r = Security.Role.FindByName(name);

			if (r == null)
				return false;

			if( u.HasRole( r ) )
				return true;

			var gs = u.GetGroups();
			if( gs != null )
			{
				foreach( var g in gs )
				{
					if( g.HasRole( r ) )
						return true;
				}
			}

			return false;
		}

		public static string GetRandomSalt(int length = 8)
		{
			string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,./;'[]-=<>?:\"{}_+`~!@#$%^&*()";
			string ret = "";
			var r = new Random();

			for( int i = 0; i < length; i++ )
				ret += chars[r.Next( 0, chars.Length )];

			return ret;
		}
	}
}
