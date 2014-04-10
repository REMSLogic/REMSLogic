using System;
using System.IO;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Web;

namespace Framework.API
{
    public class MethodInfo
    {
        public System.Reflection.MethodInfo ReflectionInfo;
        public SecurityRoleAttribute RequiresRole;
        public bool RequiresSession;
        public bool RequiresOAuth;
        public Func<HttpContext, Dictionary<string, object>, ReturnObject> runMethod;
        public Dictionary<string, Type> Parameters;

        private Regex _radioButtonArray = new Regex(@"^(?<name>[\d\w-]+)(\[)(?<id>[\d]+)(\])$");

        public MethodInfo(System.Reflection.MethodInfo mi)
        {
            Parameters = new Dictionary<string, Type>();
            ReflectionInfo = mi;
            var attrs = ReflectionInfo.GetCustomAttributes(false);
            foreach (var attr in attrs)
            {
                if (attr.GetType() == typeof(SecurityRoleAttribute))
                {
                    RequiresRole = (attr as SecurityRoleAttribute);
                    RequiresSession = true;
                }
                else if (attr.GetType() == typeof(RequireSessionAttribute))
                {
                    RequiresSession = true;
                }
                else if (attr.GetType() == typeof(OAuthSecurityAttribute))
                {
                    RequiresOAuth = true;
                }
                // Add handling for other attributes here
            }

            if (mi.IsPrivate)
                RequiresSession = true;

            var varExpresses = new List<ParameterExpression>();
            var paramToPass = new List<ParameterExpression>();

            Type contextType = typeof(HttpContext);

            var context = Expression.Parameter(contextType, "context");
            paramToPass.Add(context);

            Type inputType = typeof(Dictionary<string, object>);
            var input = Expression.Parameter(inputType, "input");
            //paramToPass.Add( input );

            var blockExpresses = new List<Expression>();

            var retLabelTarget = Expression.Label("retLabel");
            var retParam = Expression.Parameter(typeof(ReturnObject), "ret");
            var retExpress = Expression.Return(retLabelTarget);

            varExpresses.Add(retParam);

            var reqType = typeof(HttpRequest);

            var req = Expression.Parameter(reqType, "req");
            varExpresses.Add(req);

            blockExpresses.Add(Expression.Assign(req, Expression.Property(context, "Request")));

            var qsType = typeof(System.Collections.Specialized.NameValueCollection);

            var qs = Expression.Parameter(qsType, "qs");
            varExpresses.Add(qs);

            blockExpresses.Add(Expression.Assign(qs, Expression.Property(req, "QueryString")));

            var ps = mi.GetParameters();
            foreach (var p in ps)
            {
                if (p.ParameterType == typeof(HttpContext))
                    continue;

                Parameters[p.Name] = p.ParameterType;

                if (!p.ParameterType.IsClass && !TypeConversion.CanConvert(p.ParameterType))
                    throw new Exception("Can not handle API Method Parameter of type [" + p.ParameterType.FullName + "].");

                var checkBlockExpresses = new List<Expression>();
                var checkBlockVars = new List<ParameterExpression>();

                var varExpress = Expression.Parameter(p.ParameterType, p.Name);
                varExpresses.Add(varExpress);


                Expression varAssignExp = null;
                if (p.ParameterType.IsValueType)
                    varAssignExp = Expression.Assign(varExpress, Expression.Unbox(Expression.Property(input, "Item", Expression.Constant(p.Name)), p.ParameterType));
                else
                    varAssignExp = Expression.Assign(varExpress, Expression.TypeAs(Expression.Property(input, "Item", Expression.Constant(p.Name)), p.ParameterType));

                checkBlockExpresses.Add(varAssignExp);

                var checkBlock = Expression.Block(checkBlockVars, checkBlockExpresses);

                if (p.IsOptional)
                {
                    Expression varIfAssignExp = null;
                    if (p.ParameterType.IsValueType && !p.ParameterType.Name.StartsWith("Nullable"))
                        varIfAssignExp = Expression.Assign(varExpress, Expression.Constant(p.DefaultValue));
                    else
                        varIfAssignExp = Expression.Assign(varExpress, Expression.TypeAs(Expression.Constant(p.DefaultValue), p.ParameterType));

                    var ifExpress = Expression.IfThenElse(
                                            Expression.OrElse(
                                                Expression.Not(Expression.Call(input, typeof(Dictionary<string, object>).GetMethod("ContainsKey"), Expression.Constant(p.Name))),
                                                Expression.Equal(Expression.Property(input, "Item", Expression.Constant(p.Name)), Expression.Constant(null))
                                            ),
                                        varIfAssignExp,
                                        checkBlock);
                    blockExpresses.Add(ifExpress);
                }
                else
                {
                    blockExpresses.Add(checkBlock);
                }

                paramToPass.Add(varExpress);
            }

            blockExpresses.Add(Expression.Assign(retParam, Expression.Call(mi, paramToPass)));
            blockExpresses.Add(Expression.Label(retLabelTarget));
            blockExpresses.Add(retParam);

            var b = Expression.Block(varExpresses, blockExpresses);

            runMethod = Expression.Lambda<Func<HttpContext, Dictionary<String, object>, ReturnObject>>(b, context, input).Compile();
        }

        public ReturnObject Run(HttpContext context)
        {
            try
            {
                //string authorize = context.Request.Headers["Authorization"].Substring(6);
                //authorize = System.Text.Encoding.Default.GetString(Convert.FromBase64String(authorize));
                //string[] creds = authorize.Split(':');

                //BasicAuth.APICreds credentials = BasicAuth.APICreds.FindCreds(creds[0]);
                //if (credentials.password != creds[1])
                //{
                //    return new ReturnObject
                //    {
                //        StatusCode = 200,
                //        Message = creds[0].ToString() + ":" + credentials.username + "       " + creds[1].ToString() + ":" + credentials.password
                //    };
                //}

                if (RequiresSession && (context.Session == null || context.Session.IsNewSession))
                    return new ReturnObject() { StatusCode = 403, Message = "You don't have permission to do that.", Result = null };

                if (RequiresRole != null && !string.IsNullOrEmpty(RequiresRole.Role) && !Framework.Security.Manager.HasRole(RequiresRole.Role))
                    return new ReturnObject() { StatusCode = 403, Message = (string.IsNullOrEmpty(RequiresRole.ErrorMessage) ? "You must be logged in or have more permissions to do that." : RequiresRole.ErrorMessage), Result = null };

                var input = new Dictionary<string, object>();
                var oauth_input = new Dictionary<string, string>();

                foreach (string k in context.Request.QueryString.Keys)
                {
                    oauth_input[k] = context.Request.QueryString[k];

                    string name = k.Replace("-", "_");
                    object val = null;

                    if (name.EndsWith("[]"))
                    {
                        val = context.Request.QueryString[k].Split(',');
                        name = name.Substring(0, name.Length - 2);
                    }
                    else
                        val = context.Request.QueryString[k];

                    if (!Parameters.ContainsKey(name))
                        continue;

                    bool err;

                    input[name] = TypeConversion.Convert(Parameters[name], val, out err);

                    if (err)
                        return new ReturnObject() { StatusCode = 400, Message = "Can not convert parameter [" + name + "] to a " + Parameters[name].FullName + "." };
                }

                if (context.Request.HttpMethod.ToUpper() == "POST")
                {
                    if (context.Request.ContentType.ToLower().StartsWith("multipart/form-data,"))
                    {
                        string json_str = context.Request.Form["request"];

                        Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(json_str);

                        foreach (var p in json.Properties())
                        {
                            if (!Parameters.ContainsKey(p.Name))
                                continue;

                            if (p.Value.Type == Newtonsoft.Json.Linq.JTokenType.Object || p.Value.Type == Newtonsoft.Json.Linq.JTokenType.Array)
                            {
								input[p.Name] = p.Value.ToObject(Parameters[p.Name]);
                            }
                            else
                            {
                                bool err;

                                input[p.Name] = TypeConversion.Convert(Parameters[p.Name], p.Value.ToString(), out err);

                                if (err)
                                    return new ReturnObject() { StatusCode = 400, Message = "Can not convert parameter [" + p.Name + "] to a " + Parameters[p.Name].FullName + "." };
                            }
                        }
                    }
                    else if (context.Request.ContentType.ToLower().StartsWith("application/json"))
                    {
						var sr = new StreamReader(context.Request.GetBufferlessInputStream());
						string json_str = sr.ReadToEnd();
						sr.Close();
						sr = null;

						Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(json_str);

						foreach (var p in json.Properties())
						{
							string name = p.Name.Replace("-", "_");

							string id = null;
							Match m;
							bool isRadioArray = false;
							if( name.Contains( "[" ) && (m = _radioButtonArray.Match( name )).Success )
							{
								name = m.Groups["name"].Value;
								id = m.Groups["id"].Value;

								isRadioArray = true;
							}

							if (!Parameters.ContainsKey(name))
								continue;

							if( isRadioArray )
							{
								if( !input.ContainsKey( name ) )
									input.Add( name, new Dictionary<string, object>() );

								((Dictionary<string, object>)(input[name])).Add( id, p.Value.ToObject(typeof(string)) );
							}
							else if (p.Value.Type == Newtonsoft.Json.Linq.JTokenType.Object || p.Value.Type == Newtonsoft.Json.Linq.JTokenType.Array)
							{
								input[name] = p.Value.ToObject(Parameters[name]);
							}
							else
							{
								bool err;

								input[name] = TypeConversion.Convert(Parameters[name], p.Value.ToString(), out err);

								if (err)
									return new ReturnObject() { StatusCode = 400, Message = "Can not convert parameter [" + p.Name + "] to a " + Parameters[name].FullName + "." };
							}
						}
					}
					else
                    {
                        foreach (string k in context.Request.Form.Keys)
                        {
                            oauth_input[k] = context.Request.Form[k];

                            string name = k.Replace("-", "_");
                            string id;
                            object val;
                            Match m;
                            bool isRadioArray = false;

                            if (name.EndsWith("[]"))
                            {
                                val = context.Request.Form[k].Split(',');
                                name = name.Substring(0, name.Length - 2);
                            }
                            // MJL 2013-12-19 - Monk, you're going to want to check this.  Adding support for 
                            // radio button arrays. this will only run the regex check if it's very likely 
                            // we need it, should have very little impact on performance.
                            else if(name.Contains("[") && (m = _radioButtonArray.Match(name)).Success)
                            {
								// TJM 2013-12-28 - See project system - http://project.bafmin.com/index.php?c=message&a=view&id=475&active_project=122

								// Also, my screen ends ------------------------------------------------------------------------------------------------------------------------> here |
								// Well, the screen space I have for code ends there anyways.

                                // MJL 2014-01-08 - The below line broke things, the value stored in "name" does not exist
                                // in the Parameters array.  Oh, and historically lines of code and comments should not go
                                // past column 80, but hardly anyone follows that anymore.

								//var generics = Parameters[name].GetGenericArguments();

                                name = m.Groups["name"].Value;
                                id = m.Groups["id"].Value;
                                val = context.Request.Form[k];

                                isRadioArray = true;

                                if(!input.ContainsKey(name))
                                    input.Add(name, new Dictionary<string, object>());

                                // it's entirely possible for radio button arrays to be non-sequential.
                                // using a dictionary will let the consumer handle this without any proble.
                                // although the id will be number 99% of the time, this is not a techincal
                                // requirement so leave it as a string and let the consumer decided what
                                // to do with it.
                                ((Dictionary<string, object>)(input[name])).Add(id, val);
                            }
                            else
                                val = context.Request.Form[k];

                            if (!Parameters.ContainsKey(name))
                                continue;

                            // MJL 2013-12-19 - No need for the type conversion with radio button arrays.  They
                            // are intentially left as objects.  Radio button arrays are passed to methods as
                            // Dictionary<string,object>.  Typically the string will be an ID and the object a 
                            // value.

                            // needs to be initialized.  radio buttons won't do the type conversion
                            bool err = false;

                            if(!isRadioArray)
                                input[name] = TypeConversion.Convert(Parameters[name], val, out err);

                            if (err)
                                return new ReturnObject() { StatusCode = 400, Message = "Can not convert parameter [" + name + "] to a " + Parameters[name].FullName + "." };
                        }
                    }
                }

                if (RequiresOAuth)
                {
                    string oauthHeader = context.Request.Headers["Authorization"];

                    if (string.IsNullOrEmpty(oauthHeader) || !oauthHeader.StartsWith("OAuth "))
                        return new ReturnObject() { StatusCode = 403, Message = "Unauthorized. You must supply a valid OAuth Authorization header value." };

                    oauthHeader = oauthHeader.Substring(6);

                    var parts = oauthHeader.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    var oauth = new Dictionary<string, string>();

                    foreach (string part in parts)
                    {
                        var ps = part.Split('=');
                        string name = context.Server.UrlDecode(ps[0]);
                        string value = context.Server.UrlDecode(ps[1].Trim(' ', '"'));

                        if (name.StartsWith("oauth_"))
                            name = name.Substring(6);

                        oauth[name.ToLower()] = value;
                    }

                    if (oauth["version"] != "1.0")
                        return new ReturnObject() { StatusCode = 403, Message = "Invalid OAuth Authorization. Only version 1.0 is supported." };
                    if (oauth["signature_method"] != "HMAC-SHA1")
                        return new ReturnObject() { StatusCode = 403, Message = "Invalid OAuth Authorization. Only 'HMAC-SHA1' is supported as a signature method." };

                    long unix_timestamp;
                    if (!long.TryParse(oauth["timestamp"], out unix_timestamp))
                        return new ReturnObject() { StatusCode = 403, Message = "Invalid OAuth Authorization. Invalid timestamp." };

                    if ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(unix_timestamp).ToLocalTime() < DateTime.Now.AddMinutes(-15))
                        return new ReturnObject() { StatusCode = 403, Message = "Invalid OAuth Authorization. Timestamp too old." };
                }

				return runMethod(context, input);
            }
            catch (Exception exp)
            {
				Framework.Manager.LogError(exp, context);

                return new ReturnObject
                {
                    StatusCode = 500,
                    Message = exp.ToString(),
                    Result=exp
                };
            }
        }
    }
}
