using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.API;
using Lib.Data;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Lib.API.Admin.Security
{
    public class Ecommerce : Base
    {
        [SecurityRole("view_admin")]
        [Method("Admin/Security/Ecommerce/EditProvider")]
        public static ReturnObject EditProvider(HttpContext context, long provider_user_id, string username, string password, string email, string first_name, string last_name, string street, string city, string state, string zip, string expires_on, string is_enabled, string street_2 = null, string phone = null)
        {
            IAccountService accountSvc = ObjectFactory.GetInstance<IAccountService>();

            Lib.Data.Provider provider;
            Lib.Data.ProviderUser providerUser;

            UserProfile userProfile;
            Contact contact;
            Address address;
            Account account;

            Framework.Security.User user;

            if (provider_user_id > 0)
            {
                providerUser = new Lib.Data.ProviderUser(provider_user_id);
                provider = providerUser.Provider;
                userProfile = providerUser.Profile;
                user = userProfile.User;
                contact = userProfile.PrimaryContact;
                address = userProfile.PrimaryAddress;

                account = accountSvc.GetByUserProfileId(userProfile.ID ?? 0);

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

                account = new Account
                {
                    CreatedAt = DateTime.Now
                };

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

            DateTime expiresOn;

            if(!DateTime.TryParse(expires_on, out expiresOn))
            {
                    return new ReturnObject()
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = "Invalide expiration date."
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
            userProfile.IsEcommerce = true;
            userProfile.Save();

            providerUser.ProfileID = userProfile.ID.Value;
            providerUser.ProviderID = provider.ID.Value;
            providerUser.OrganizationID = 0;
            providerUser.ProviderUserType = "";
            providerUser.PrimaryFacilityID = 0;
            providerUser.Save();

            account.UserProifleId = userProfile.ID ?? 0;
            account.ExpiresOn = expiresOn;
            account.IsEnabled = is_enabled == "yes";
            
            accountSvc.Save(account);

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
		[Method("Admin/Security/Ecommerce/DeleteProvider")]
		public static ReturnObject DeleteProvider(HttpContext context, long id)
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

        [SecurityRole("view_admin")]
        [Method("Admin/Security/Ecommerce/EditPrescriberProfile")]
        public static ReturnObject Edit( HttpContext context, long provider_id, long profile_id, string first_name, string last_name, string email, string phone, 
            string street_1, string city, string state, string zip, string npi, string state_id, long issuer, long speciality, long prescriber_type, string username, string password, string confirm_password, string expires_on, string is_enabled, string street_2 = null, string fax = null)
        {
            IAccountService accountSvc = ObjectFactory.GetInstance<IAccountService>();

            UserProfile userProfile;
            PrescriberProfile prescriberProfile;
            Data.Prescriber prescriber;
            Address address;
            Contact contact;
            Account account;

            Framework.Security.User user;

            if (profile_id > 0)
            {
                prescriberProfile = new PrescriberProfile(profile_id);
                prescriber = prescriberProfile.Prescriber;
                userProfile = prescriber.Profile;
                user = userProfile.User;
                address = userProfile.PrimaryAddress;
                contact = userProfile.PrimaryContact;
                account = accountSvc.GetByUserProfileId(userProfile.ID ?? 0);
            }
            else
            {
                userProfile = new UserProfile();
                userProfile.Created = DateTime.Now;
                prescriberProfile = new PrescriberProfile();
                prescriber = new Data.Prescriber();
                contact = new Contact();
                user = new Framework.Security.User();
                address = new Address();

                account = new Account
                {
                    CreatedAt = DateTime.Now
                };
            }

            if (!user.ID.HasValue && string.IsNullOrEmpty(password))
            {
                return new ReturnObject()
                {
                    Error = true,
                    StatusCode = 200,
                    Message = "If you are creating a new prescriber, you must enter a password."
                };
            }

            if (!string.IsNullOrEmpty(password) )
            {
                if (password != confirm_password)
                {
                    return new ReturnObject()
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = "The passwords you entered do no match."
                    };
                }
                else
                {
                    user.PasswordSalt = Framework.Security.Manager.GetRandomSalt();
                    user.Password = Framework.Security.Hash.GetSHA512(password + user.PasswordSalt);
                }
            }

            user.Username = username;
            user.Email = email;
            user.Save();

            IList<Framework.Security.Group> userGroups = user.GetGroups();

            if(!userGroups.Any(x => x.ID == 2))
                user.AddGroup(new Framework.Security.Group(2));

            if(!userGroups.Any(x => x.ID == 3))
                user.AddGroup(new Framework.Security.Group(3));

            contact.Email = email;
            contact.Phone = phone;
            contact.FirstName = first_name;
            contact.LastName = last_name;
            contact.Save();

            DateTime expiresOn;

            if(!DateTime.TryParse(expires_on, out expiresOn))
            {
                    return new ReturnObject()
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = "Invalide expiration date."
                    };
            }

            address.Street1 = street_1;
            address.Street2 = street_2;
            address.City = city;
            address.State = state;
            address.Zip = zip;
            address.Country = "United States";
            address.Save();

            userProfile.UserID = user.ID.Value;
            userProfile.UserTypeID = 0;
            userProfile.PrimaryAddressID = address.ID.Value;
            userProfile.PrimaryContactID = contact.ID.Value;
            userProfile.IsEcommerce = true;
            userProfile.Save();

            prescriber.ProfileID = userProfile.ID.Value;
            prescriber.SpecialityID = speciality;
            prescriber.NpiId = npi;
            prescriber.StateId = state_id;
            prescriber.StateIdIssuer = issuer;
            prescriber.Save();

            prescriberProfile.PrescriberID = prescriber.ID;
            prescriberProfile.ProviderID = provider_id;
            prescriberProfile.AddressID = address.ID.Value;
            prescriberProfile.ContactID = contact.ID.Value;
            prescriberProfile.PrescriberTypeID = prescriber_type;
            prescriberProfile.PrimaryFacilityID = 0;
            prescriberProfile.Expires = DateTime.Now.AddYears(1);
            prescriberProfile.OrganizationId = provider_id;
            prescriberProfile.Save();

            account.UserProifleId = userProfile.ID ?? 0;
            account.ExpiresOn = expiresOn;
            account.IsEnabled = is_enabled == "yes";
            
            accountSvc.Save(account);

            return new ReturnObject()
            {
                Result = prescriber,
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully saved this Prescriber.",
                        title = "Prescriber Saved"
                    }
                }
            };
        }

        [SecurityRole("view_admin")]
        [Method("Admin/Security/Ecommerce/DeletePrescriber")]
        public static ReturnObject DeletePrescriber(HttpContext context, long id)
        {
            if (id <= 0)
                return new ReturnObject() { Error = true, Message = "Invalid Prescriber." };

            var item = new PrescriberProfile(id);

            item.Address.Delete();
            item.Contact.Delete();
            item.Delete();

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully deleted a Prescriber.",
                        title = "Prescriber deleted"
                    }
                },
                Actions = new List<ReturnActionObject>()
                {
                    new ReturnActionObject() {
                        Ele = "#prescribers-table tr[data-id=\""+id.ToString()+"\"]",
                        Type = "remove"
                    }
                }
            };
        }
    }
}
