using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
    public class Speciality : Base
    {
        [SecurityRole("dev")]
        [Method("Admin/Security/Speciality/Edit")]
        public static ReturnObject Edit(HttpContext context, long id, string name, string code)
        {
            Data.Speciality item = null;

            item = id > 0 
                ? new Data.Speciality(id) 
                : new Data.Speciality();

            item.Name = name.Trim();
            item.Code = code.Trim();
            item.Save();

            return new ReturnObject() {
                Result = item,
                Redirect = new ReturnRedirectObject() {
                    Hash = "dev/specialities/list"
                },
                Growl = new ReturnGrowlObject() {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject() {
                        text = "You have successfully saved this speciality.",
                        title = "Speciality Saved"
                    }
                }
            };
        }

        [SecurityRole("dev")]
        [Method("Admin/Security/Speciality/Delete")]
        public static ReturnObject Delete(HttpContext context, long id)
        {
            if (id <= 0)
                return new ReturnObject() { Error = true, Message = "Invalid Speciality." };

            var item = new Data.Speciality(id);
            item.Delete();

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully deleted a speciality.",
                        title = "Speciality Deleted"
                    }
                },
                Actions = new List<ReturnActionObject>()
                {
                    new ReturnActionObject() {
                        Ele = "#specialities-table tr[data-id=\""+id.ToString(CultureInfo.InvariantCulture)+"\"]",
                        Type = "remove"
                    }
                }
            };
        }
    }
}
