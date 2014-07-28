using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Framework;
using Framework.API;
using Lib.Data;
using Lib.Systems;

namespace Lib.API.Provider
{
    public class Prescribers : Base
    {
        [SecurityRole("view_provider")]
        [Method("Provider/Prescribers/Create")]
        public static ReturnObject Create(HttpContext context, string email, string first_name, string last_name, string phone_number, string message = null)
        {
			var provider = Security.GetCurrentProvider();
            var providerProfile = ProviderUser.FindByProvider(provider).FirstOrDefault();

			if( provider == null || string.IsNullOrEmpty(email) )
				return new ReturnObject() { Error = true, Message = "Invalid Request." };
            
            var contact = new Contact
            {
                FirstName = first_name,
                LastName = last_name,
                Email = email,
				Phone = phone_number,
				Fax = null
            };
            contact.Save();

            var address = new Address
            {
                Street1 = string.Empty,
				Street2 = null,
				City = string.Empty,
				State = string.Empty,
				Zip = string.Empty,
				Country = string.Empty
            };
            address.Save();

            var prescriberProf = new PrescriberProfile
            {
                Guid = Guid.NewGuid(),
                ProviderID = provider.ID.Value,
				ContactID = contact.ID.Value,
                AddressID = address.ID.Value,
                Expires = DateTime.Now.AddYears(1),
                PrimaryFacilityID = providerProfile.PrimaryFacilityID,
                OrganizationId = providerProfile.OrganizationID,
                Deleted = false,
            };

            prescriberProf.Save();

			var data = new Dictionary<string, object> {
                {"Message", (message != null)? message : "You have been invited to use the REMS Logic system.  Please click the link below to complete your profile"},
				{"Token", prescriberProf.Guid},
				{"Year", DateTime.Now.Year.ToString()},
				{"EmailAddress", email}
			};

			var overrides = new Framework.Email.TemplateOverrides {
				To = new [] { new MailAddress(email) }
			};

            Email.SendTemplate("PrescriberInvite", data, overrides);

            return new ReturnObject
            {
                Result = prescriberProf,
				Actions = new List<ReturnActionObject>(new ReturnActionObject[] {
					new ReturnActionObject { Type = "back" }
				}),
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "Your invite has been sent",
                        title = "Prescriber Invited"
                    }
                }
            };
        }

        [SecurityRole("view_provider")]
        [Method("Provider/Prescribers/Edit")]
		public static ReturnObject Edit( HttpContext context, long profile_id, string first_name, string last_name, string email, string phone, 
            string street_1, string city, string state, string zip, string npi, string state_id, long issuer, long speciality, long prescriber_type, string street_2 = null, string fax = null, long? 
            facility_id = null)
        {
			var profile = new Data.PrescriberProfile( profile_id );

			if( profile.ID != profile_id )
				return new ReturnObject() { Error = true, Message = "Invalid Prescriber." };

			var provider = Lib.Systems.Security.GetCurrentProvider();
            var prescriber = profile.Prescriber;

			if( provider == null || profile.ProviderID != provider.ID )
				return new ReturnObject() { Error = true, Message = "Invalid Prescriber." };

			profile.Contact.FirstName = first_name;
			profile.Contact.LastName = last_name;
			profile.Contact.Email = email;
			profile.Contact.Phone = phone;
			profile.Contact.Fax = fax;
            profile.PrescriberTypeID = prescriber_type;
            profile.Contact.Save();

			profile.Address.Street1 = street_1;
			profile.Address.Street2 = street_2;
			profile.Address.City = city;
			profile.Address.State = state;
			profile.Address.Zip = zip;
			profile.Address.Country = "USA";
            profile.Address.Save();

			profile.PrimaryFacilityID = facility_id;
			profile.Save();

            prescriber.NpiId = npi;
            prescriber.StateId = state_id;
            prescriber.StateIdIssuer = issuer;
            prescriber.SpecialityID = speciality;
            prescriber.Save();

            return new ReturnObject
            {
				Result = profile,
				Actions = new List<ReturnActionObject>(new ReturnActionObject[] {
					new ReturnActionObject { Type = "back" }
				}),
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = profile.Contact.Name+" has been saved.",
                        title = "Prescriber Updated"
                    }
                }
            };
        }

        [SecurityRole("view_provider")]
        [Method("Provider/Prescribers/Delete")]
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
