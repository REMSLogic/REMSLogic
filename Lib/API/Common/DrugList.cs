using Framework.API;
using Framework.Security;
using RemsLogic.Model;
using RemsLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Lib.API.Common
{
    public class DrugList : Base
    {
        [Method("Common/DrugList/AddDrugToFavorites")]
        public static ReturnObject AddDrugToFavorites(HttpContext context, long id)
        {
            IDrugListRepository dlRepo = new DrugListRepository(ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString);

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlRepo.AddDrugToFavoritesByProfileId(profile.ID.Value, id);
            }

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "Your changes have been saved.",
                        title = "Drug Added to Favorites"
                    }
                }
            };
        }

        [Method("Common/DrugList/RemoveDrugFromFavorites")]
        public static ReturnObject RemoveDrugFromFavorites(HttpContext context, long id)
        {
            IDrugListRepository dlRepo = new DrugListRepository(ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString);

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlRepo.RemoveDrugFromFavoritesByProfileId(profile.ID.Value, id);
            }

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "Your changes have been saved.",
                        title = "Drug Removed from Favorites"
                    }
                }
            };
        }

        [Method("Common/DrugList/AddDrugToList")]
        public static ReturnObject AddDrugToList(HttpContext context, long id)
        {
            IDrugListRepository dlRepo = new DrugListRepository(ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString);

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlRepo.AddDrugToDrugListByProfileId(profile.ID.Value, id);
            }

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "Your changes have been saved.",
                        title = "Drug Added to Drug List"
                    }
                }
            };
        }

        [Method("Common/DrugList/RemoveDrugFromList")]
        public static ReturnObject RemoveDrugFromList(HttpContext context, long id)
        {
            IDrugListRepository dlRepo = new DrugListRepository(ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString);

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlRepo.RemoveDrugFromDrugListByProfileId(profile.ID.Value, id);
            }

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "Your changes have been saved.",
                        title = "Drug Removed from Drug List"
                    }
                }
            };
        }
    }
}
