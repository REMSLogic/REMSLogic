using System;
using System.Web;
using Framework.API;
using RemsLogic.Model;
using RemsLogic.Repositories;
using StructureMap;

namespace Lib.API.Admin
{
    public class RestrictedLinks : Base
    {
        [SecurityRole("view_admin")]
        [Method("Admin/RestrictedLinks/Edit")]
        public static ReturnObject Edit(HttpContext context, string url, string token, string expiration_date, string created_for)
        {
            IRestrictedLinkRepository linkRepo = ObjectFactory.GetInstance<IRestrictedLinkRepository>();

            linkRepo.Save(new RestrictedLink
            {
                Url = url,
                Token = Guid.Parse(token),
                ExpirationDate = DateTime.Parse(expiration_date),
                CreatedFor = created_for
            });

            var ret = new ReturnObject {
                Growl = new ReturnGrowlObject {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject {
                        text = "You have successfully created a new video link.",
                        title = "File Uploaded"
                    }
                },
                Redirect = new ReturnRedirectObject {
                    Hash = "admin/video-links/list"
                }
            };

            return ret;
        }
    }
}
