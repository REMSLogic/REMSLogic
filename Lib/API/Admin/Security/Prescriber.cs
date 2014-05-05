using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;
using Lib.Data;

namespace Lib.API.Admin.Security
{
    public class Prescriber : Base
    {
        [SecurityRole("view_admin")]
        [Method("Admin/Security/Prescriber/Edit")]
        public static ReturnObject Edit( HttpContext context, long provider_id, long facility_id, long profile_id, string first_name, string last_name, string email, string phone, 
            string street_1, string city, string state, string zip, string npi, string state_id, long issuer, long speciality, long prescriber_type, string username, string password, string confirm_password, string street_2 = null, string fax = null)
        {
            UserProfile userProfile;
            PrescriberProfile prescriberProfile;
            Data.Prescriber prescriber;
            Address address;
            Contact contact;

            Framework.Security.User user;

            if (profile_id > 0)
            {
                prescriberProfile = new PrescriberProfile(profile_id);
                prescriber = prescriberProfile.Prescriber;
                userProfile = prescriber.Profile;
                user = userProfile.User;
                address = userProfile.PrimaryAddress;
                contact = userProfile.PrimaryContact;
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

            contact.Email = email;
            contact.Phone = phone;
            contact.FirstName = first_name;
            contact.LastName = last_name;
            contact.Save();

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
            prescriberProfile.PrimaryFacilityID = facility_id;
            prescriberProfile.Expires = DateTime.Now.AddYears(1);
            prescriberProfile.Save();

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
        [Method("Admin/Security/Prescriber/Delete")]
        public static ReturnObject Delete(HttpContext context, long id)
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
