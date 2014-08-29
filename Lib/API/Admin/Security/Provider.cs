using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;
using Address = Lib.Data.Address;

namespace Lib.API.Admin.Security
{
	public class Provider : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Security/Provider/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string name, string facility_size, string fac_name, string fac_street1, string fac_street2, string fac_city, string fac_state, string fac_zip, string fac_country)
		{
            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();

            Organization org = orgSvc.Get(id) ?? new Organization
            {
                PrimaryFacility = new Facility
                {
                    Address = new RemsLogic.Model.Address()
                }
            };

            org.Name = name;

            org.PrimaryFacility.Name = fac_name;
            org.PrimaryFacility.BedSize = facility_size;

            org.PrimaryFacility.Address.Street1 = fac_street1;
            org.PrimaryFacility.Address.Street2 = fac_street2;
            org.PrimaryFacility.Address.City = fac_city;
            org.PrimaryFacility.Address.State = fac_state;
            org.PrimaryFacility.Address.Zip = fac_zip;
            org.PrimaryFacility.Address.Country = fac_country;

            orgSvc.Save(org);

            return new ReturnObject()
            {
                Result = org,
                Redirect = new ReturnRedirectObject()
                {
                    Hash = "admin/security/providers/list"
                },
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "You have successfully saved this Organization.",
                        title = "Organization Saved"
                    }
                }
            };
        }

        [SecurityRole("view_admin")]
        [Method("Admin/Security/Provider/Delete")]
        public static ReturnObject Delete(HttpContext context, long id)
        {
            if (id <= 0)
                return new ReturnObject() { Error = true, Message = "Invalid Organization." };

            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();

            orgSvc.Delete(id);

            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully deleted a provider.",
                        title = "Provider deleted"
                    }
                },
                Actions = new List<ReturnActionObject>()
                {
                    new ReturnActionObject() {
                        Ele = "#providers-table tr[data-id=\""+id.ToString()+"\"]",
                        Type = "remove"
                    }
                }
            };
        }

		[SecurityRole( "view_admin" )]
		[Method( "Admin/Security/Provider/UploadPrescribers" )]
		public static ReturnObject UploadPrescribers( HttpContext context, long id )
		{
			if( id <= 0 )
				return new ReturnObject() { Error = true, Message = "Invalid Organization." };

            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
			Organization organization = orgSvc.Get(id);

			if(organization == null)
				return new ReturnObject() { Error = true, Message = "Invalid Organization." };

			if( context.Request.Files.Count <= 0 || context.Request.Files["prescribers-csv"] == null )
				return new ReturnObject() { Error = true, Message = "Invalid Upload." };

			int count = 0;
			var stream = context.Request.Files["prescribers-csv"].InputStream;

			var csv = new CsvHelper.CsvReader( new StreamReader( stream, Encoding.ASCII ) );
			csv.Configuration.IsHeaderCaseSensitive = false;
			csv.Configuration.SkipEmptyRecords = true;
			csv.Configuration.TrimFields = true;
			csv.Configuration.TrimHeaders = true;

			csv.Configuration.RegisterClassMap<PrescriberCsvClassMap>();
			List<PresciberCsv> rows;

			try
			{
				rows = csv.GetRecords<PresciberCsv>().ToList();
			}
			catch( Exception ex )
			{
				Framework.Manager.LogError( ex, context );

				return new ReturnObject() { Error = true, Message = "There was a problem with the Csv File you provided." };
			}

			foreach( var row in rows )
			{
                var curRow = row;
                Facility facility = (
                    from f in organization.Facilities
                    where String.Equals(f.Name, curRow.FacilityName, StringComparison.InvariantCultureIgnoreCase)
                    select f).FirstOrDefault();

                if(facility == null)
                {
                    return new ReturnObject
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = "Unknown facilty name."
                    };
                }

				var contact = new Data.Contact();
				contact.FirstName = row.FirstName;
				contact.LastName = row.LastName;
				contact.Email = row.Email;
				contact.Phone = row.Phone;
				contact.Fax = row.Fax;
				contact.Save();

				var address = new Data.Address();
				address.Street1 = row.Street1;
				address.Street2 = row.Street2;
				address.City = row.City;
				address.State = row.State;
				address.Zip = row.Zip;
				address.Country = row.Country;
				address.Save();

				var profile = new Data.PrescriberProfile();
				profile.Guid = Guid.NewGuid();
				profile.ProviderID = organization.Id;
				profile.ContactID = contact.ID.Value;
				profile.AddressID = address.ID.Value;
				profile.Expires = DateTime.Now.AddYears( 1 );
                profile.PrimaryFacilityID = facility.Id;
                profile.OrganizationId = organization.Id;
				profile.Deleted = false;
				profile.Save();

				var data = new Dictionary<string, object> {
					{"Token", profile.Guid},
					{"Year", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture)},
                    {"Message", String.Format("{0} has invited you to join REMS Logic.  Please follow the link below to setup your prescriber profile.", organization.Name)},
					{"EmailAddress", contact.Email}
				};

				var overrides = new Framework.Email.TemplateOverrides {
					To = new[] { new System.Net.Mail.MailAddress( contact.Email ) }
				};

				Framework.Email.SendTemplate( "PrescriberInvite", data, overrides );

				count++;
			}

			return new ReturnObject() {
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "Successfully imported " + count+ " prescribers.",
						title = "Import Successful"
					}
				}
			};
		}

		private class PresciberCsv
		{
            // Orgnization info
            public string FacilityName {get; set;}

			// Contact info
			public string FirstName {get; set;}
			public string LastName {get; set;}
			public string Email {get; set;}
			public string Phone {get; set;}
			public string Fax {get; set;}

			// Address Info
			public string Street1 {get; set;}
			public string Street2 {get; set;}
			public string City {get; set;}
			public string State {get; set;}
			public string Zip {get; set;}
			public string Country {get; set;}
		}

		private class PrescriberCsvClassMap : CsvHelper.Configuration.CsvClassMap<PresciberCsv>
		{
			public override void CreateMap()
			{
                Map(m => m.FacilityName).Name("fac", "facility", "facname");

				Map( m => m.FirstName ).Name( "first name", "firstname", "f name", "fname" );
				Map( m => m.LastName ).Name( "last name", "lastname", "l name", "lname" );
				Map( m => m.Email ).Name( "email" );
				Map( m => m.Phone ).Name( "phone", "phone number", "phonenumber" );
				Map( m => m.Fax ).Name( "fax", "fax number", "faxnumber" );

				Map( m => m.Street1 ).Name( "street 1", "street1", "address1", "address 1" );
				Map( m => m.Street2 ).Name( "street 2", "street2", "address2", "address 2" );
				Map( m => m.City ).Name( "city" );
				Map( m => m.State ).Name( "state", "province" );
				Map( m => m.Zip ).Name( "zip", "zipcode", "zip code", "postal", "postalcode", "postal code" );
				Map( m => m.Country ).Name( "country" ).Default("USA");
			}
		}

        [SecurityRole( "view_admin" )]
        [Method( "Admin/Security/Provider/UploadFacilities" )]
        public static ReturnObject UploadFacilities( HttpContext context, long id )
        {
            if( id <= 0 )
                return new ReturnObject() { Error = true, Message = "Invalid Organization." };

            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
            Organization organization = orgSvc.Get(id);

            if(organization == null)
                return new ReturnObject { Error = true, Message = "Invalid Organization." };

            if( context.Request.Files.Count <= 0 || context.Request.Files["facilities-csv"] == null )
                return new ReturnObject { Error = true, Message = "Invalid Upload." };

            int count = 0;
            var stream = context.Request.Files["facilities-csv"].InputStream;

            var csv = new CsvHelper.CsvReader( new StreamReader( stream, Encoding.ASCII ) );
            csv.Configuration.IsHeaderCaseSensitive = false;
            csv.Configuration.SkipEmptyRecords = true;
            csv.Configuration.TrimFields = true;
            csv.Configuration.TrimHeaders = true;

            csv.Configuration.RegisterClassMap<FacilityCsvClassMap>();
            List<FacilityCsv> rows;

            try
            {
                rows = csv.GetRecords<FacilityCsv>().ToList();
            }
            catch( Exception ex )
            {
                Framework.Manager.LogError( ex, context );

                return new ReturnObject { Error = true, Message = "There was a problem with the Csv File you provided." };
            }

            foreach( var row in rows )
            {
                var facility = new Facility
                {
                    OrganizationId = id,
                    Name = row.Name,
                    BedSize = row.BedSize,
                    Address = new RemsLogic.Model.Address
                    {
                        Street1 = row.Street1,
                        Street2 = row.Street2,
                        City = row.City,
                        State = row.State,
                        Zip = row.Zip,
                        Country = row.Country
                    }
                };

                orgSvc.SaveFacility(facility);
                count++;
            }

            return new ReturnObject {
                Growl = new ReturnGrowlObject() {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject() {
                        text = "Successfully imported " + count+ " facilities.",
                        title = "Import Successful"
                    }
                }
            };
        }

        private class FacilityCsv
        {
            public string Name {get; set;}
            public string BedSize {get; set;}
            public string Street1 {get; set;}
            public string Street2 {get; set;}
            public string City {get; set;}
            public string State {get; set;}
            public string Zip {get; set;}
            public string Country {get; set;}
        }

        private class FacilityCsvClassMap : CsvHelper.Configuration.CsvClassMap<FacilityCsv>
        {
            public override void CreateMap()
            {
                Map( m => m.Name ).Name( "name", "facility", "facility", "fname");
                Map( m => m.BedSize ).Name("bedsize", "bed size", "bsize");
                Map( m => m.Street1 ).Name( "street 1", "street1", "address1", "address 1" );
                Map( m => m.Street2 ).Name( "street 2", "street2", "address2", "address 2" );
                Map( m => m.City ).Name( "city" );
                Map( m => m.State ).Name( "state", "province" );
                Map( m => m.Zip ).Name( "zip", "zipcode", "zip code", "postal", "postalcode", "postal code" );
                Map( m => m.Country ).Name( "country" ).Default("USA");
            }
        }

        [SecurityRole( "view_admin" )]
        [Method( "Admin/Security/Provider/UploadUsers" )]
        public static ReturnObject UploadUsers( HttpContext context, long id )
        {
            if( id <= 0 )
                return new ReturnObject() { Error = true, Message = "Invalid Organization." };

            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
            Organization organization = orgSvc.Get(id);

            if(organization == null)
                return new ReturnObject { Error = true, Message = "Invalid Organization." };

            if( context.Request.Files.Count <= 0 || context.Request.Files["users-csv"] == null )
                return new ReturnObject { Error = true, Message = "Invalid Upload." };

            int count = 0;
            var stream = context.Request.Files["users-csv"].InputStream;

            var csv = new CsvHelper.CsvReader( new StreamReader( stream, Encoding.ASCII ) );
            csv.Configuration.IsHeaderCaseSensitive = false;
            csv.Configuration.SkipEmptyRecords = true;
            csv.Configuration.TrimFields = true;
            csv.Configuration.TrimHeaders = true;

            csv.Configuration.RegisterClassMap<UserCsvClassMap>();
            List<UserCsv> rows;

            try
            {
                rows = csv.GetRecords<UserCsv>().ToList();
            }
            catch( Exception ex )
            {
                Framework.Manager.LogError( ex, context );

                return new ReturnObject { Error = true, Message = "There was a problem with the Csv File you provided." };
            }

            foreach(var row in rows )
            {
                //Data.Provider provider = new Data.Provider(id);
                Lib.Data.ProviderUser providerUser = new Data.ProviderUser();
                UserProfile profile = new UserProfile {Created = DateTime.Now};
                Contact contact = new Contact();
                Address address = new Address();

                string error;
                Framework.Security.User user = Framework.Security.Manager.CreateUser(row.Username, row.Password, row.Email, out error);

                user.AddGroup(Framework.Security.Group.FindByName("users"));
                user.AddGroup(Framework.Security.Group.FindByName("providers"));

                if (!string.IsNullOrEmpty(error))
                {
                    return new ReturnObject
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = error
                    };
                }

                if (row.UserType != "technical" && row.UserType != "administrative")
                {
                    return new ReturnObject
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = "Invalid user type."
                    };
                }

                var curRow = row;
                Facility facility = (
                    from f in organization.Facilities
                    where String.Equals(f.Name, curRow.FacilityName, StringComparison.InvariantCultureIgnoreCase)
                    select f).FirstOrDefault();

                if(facility == null)
                {
                    return new ReturnObject
                    {
                        Error = true,
                        StatusCode = 200,
                        Message = "Unknown facilty name."
                    };
                }

                address.Street1 = row.Street1;
                address.Street2 = row.Street2;
                address.City = row.City;
                address.State = row.State;
                address.Zip = row.Zip;
                address.Country = row.Country;
                address.Save();

                contact.Email = row.Email;
                contact.FirstName = row.FirstName;
                contact.LastName = row.LastName;
                contact.Phone = row.Phone;
                contact.Save();

                var ut = Data.UserType.FindByName("provider");

                profile.UserTypeID = ut.ID.Value;
                profile.UserID = user.ID.Value;
                profile.PrimaryAddressID = address.ID.Value;
                profile.PrimaryContactID = contact.ID.Value;
                profile.Save();

                providerUser.ProfileID = profile.ID.Value;
                providerUser.ProviderID = id;
                providerUser.ProviderUserType = row.UserType;
                providerUser.OrganizationID = id;
                providerUser.PrimaryFacilityID = facility.Id;
                providerUser.Class = Data.ProviderUser.ProviderClass.Standard;
                providerUser.Save();

                count++;
            }

            return new ReturnObject {
                Growl = new ReturnGrowlObject() {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject() {
                        text = "Successfully imported " + count+ " Users.",
                        title = "Import Successful"
                    }
                }
            };
        }

        private class UserCsv
        {
            public string FacilityName {get; set;}
            public string FirstName {get; set;}
            public string LastName {get; set;}
            public string Email {get; set;}
            public string Phone {get; set;}
            public string Username {get; set;}
            public string UserType {get; set;}
            public string Password {get; set;}
            public string Street1 {get; set;}
            public string Street2 {get; set;}
            public string City {get; set;}
            public string State {get; set;}
            public string Zip {get; set;}
            public string Country {get; set;}
        }

        private class UserCsvClassMap : CsvHelper.Configuration.CsvClassMap<UserCsv>
        {
            public override void CreateMap()
            {
                Map(m => m.FacilityName).Name("fac", "facility", "facname");
                Map(m => m.FirstName).Name( "first name", "firstname", "f name", "fname" );
                Map(m => m.LastName).Name( "last name", "lastname", "l name", "lname" );
                Map(m => m.Email).Name( "email" );
                Map(m => m.Phone).Name( "phone", "phone number", "phonenumber" );
                Map(m => m.Username).Name("username", "user name", "login" );
                Map(m => m.UserType).Name("usertype", "type", "user type" );
                Map(m => m.Password).Name("password", "pw");
                Map(m => m.Street1).Name( "street 1", "street1", "address1", "address 1" );
                Map(m => m.Street2).Name( "street 2", "street2", "address2", "address 2" );
                Map(m => m.City).Name( "city" );
                Map(m => m.State).Name( "state", "province" );
                Map(m => m.Zip).Name( "zip", "zipcode", "zip code", "postal", "postalcode", "postal code" );
                Map(m => m.Country).Name( "country" ).Default("USA");
            }
        }
	}
}
