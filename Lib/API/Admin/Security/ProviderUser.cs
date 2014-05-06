using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;
using Lib.Data;

namespace Lib.API.Admin.Security
{
	public class ProviderUser : Base
	{
        [SecurityRole("view_admin")]
        [Method("Admin/Security/ProviderUser/Edit")]
        public static ReturnObject Edit(HttpContext context, long provider_user_id, long organization_id, long facility_id, string user_type, string username, string password, string email, string first_name, string last_name, string street, string city, string state, string zip, string street_2 = null, string phone = null)
        {
            Lib.Data.Provider provider;
            Lib.Data.ProviderUser providerUser;

            UserProfile userProfile;
            Contact contact;
            Address address;

            Framework.Security.User user;

            if (provider_user_id > 0)
            {
                providerUser = new Lib.Data.ProviderUser(provider_user_id);
                provider = providerUser.Provider;
                userProfile = providerUser.Profile;
                user = userProfile.User;
                contact = userProfile.PrimaryContact;
                address = userProfile.PrimaryAddress;

                user.Username = username;
                user.Save();

                Framework.Security.Manager.SetPassword(user, password);
            }
            else
            {
                provider = new Lib.Data.Provider();
                providerUser = new Lib.Data.ProviderUser();
                userProfile = new Data.UserProfile();
                userProfile.Created = DateTime.Now;
                contact = new Data.Contact();
                address = new Data.Address();

                string error = "";
                user = Framework.Security.Manager.CreateUser(username, password, email, out error);

                user.AddGroup(Framework.Security.Group.FindByName("users"));
                user.AddGroup(Framework.Security.Group.FindByName("providers"));

                if (!string.IsNullOrEmpty(error))
                {
                    return new ReturnObject()
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = error
                    };
                }
            }

            if (user_type != "technical" && user_type != "administrative")
            {
                return new ReturnObject()
                {
                    Error = true,
                    StatusCode = 200,
                    Message = "Invalid user type."
                };
            }

            address.Street1 = street;
            address.Street2 = street_2;
            address.City = city;
            address.State = state;
            address.Zip = zip;
            address.Country = "United States";
            address.Save();

            contact.Email = email;
            contact.FirstName = first_name;
            contact.LastName = last_name;
            contact.Phone = phone;
            contact.Save();

            provider.AddressID = address.ID;
            provider.PrimaryContactID = contact.ID;
            provider.Created = DateTime.Now;
            provider.FacilitySize = String.Empty;
            provider.Name = string.Empty;
            provider.Save();

            var ut = Lib.Data.UserType.FindByName("provider");

            userProfile.UserTypeID = ut.ID.Value;
            userProfile.UserID = user.ID.Value;
            userProfile.PrimaryAddressID = address.ID.Value;
            userProfile.PrimaryContactID = contact.ID.Value;
            userProfile.Save();

            providerUser.ProfileID = userProfile.ID.Value;
            providerUser.ProviderID = provider.ID.Value;
            providerUser.OrganizationID = organization_id;
            providerUser.ProviderUserType = user_type;
            providerUser.PrimaryFacilityID = facility_id;
            providerUser.Save();

            return new ReturnObject()
            {
                Result = providerUser,
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully saved this provider user.",
                        title = "Provider User Saved"
                    }
                }
            };
        }

		[SecurityRole("view_admin")]
		[Method("Admin/Security/ProviderUser/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Provider." };

			var item = new Lib.Data.ProviderUser(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a provider user.",
						title = "Provider User deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#users-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
