using Framework.API;
using Framework.Security;
using RemsLogic.Model;
using RemsLogic.Repositories;
using RemsLogic.Services;
using System.Configuration;
using System.Web;
using StructureMap;

namespace Lib.API.Common
{
    public class DrugList : Base
    {
        [Method("Common/DrugList/AddDrugToFavorites")]
        public static ReturnObject AddDrugToFavorites(HttpContext context, long id)
        {
            IDrugListService dlService = ObjectFactory.GetInstance<IDrugListService>();

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlService.AddDrugToDrugListByProfileId(profile.ID.Value, id, DrugListType.Favorites);
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
            IDrugListService dlService = ObjectFactory.GetInstance<IDrugListService>();

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlService.RemoveDrugFromDrugListByProfileId(profile.ID.Value, id, DrugListType.Favorites);
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
            // MJL - It doesn't appear that this is used?
            IDrugListService dlService = ObjectFactory.GetInstance<IDrugListService>();

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlService.AddDrugToDrugListByProfileId(profile.ID.Value, id, DrugListType.MyDrugs);
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
            IDrugListService dlService = ObjectFactory.GetInstance<IDrugListService>();

            User user = Framework.Security.Manager.GetUser();
            var profile = Data.UserProfile.FindByUser(user);

            if (profile != null && profile.ID != null)
            {
                dlService.RemoveDrugFromDrugListByProfileId(profile.ID.Value, id, DrugListType.MyDrugs);
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
