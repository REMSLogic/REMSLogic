using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using Framework.API;
using Framework.Security;
using Lib.Data;
using Lib.Systems.Notifications;

namespace Lib.API.Prescriber
{
    public class Wizards : Base
    {
        [Method("Prescriber/Wizards/AttachProfile")]
        public static ReturnObject AttachProfile(HttpContext context, long id, string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) )
                return Failure(401, "Invalid username or password.");

            if(!Framework.Security.Manager.Login(username, password))
                return Failure(401, "Invalid username or password.");

            UserProfile userProfile = UserProfile.FindByUser(Framework.Security.Manager.GetUser());
            Data.Prescriber prescriber = Data.Prescriber.FindByProfile(userProfile);
            PrescriberProfile prescriberProfile = new PrescriberProfile(id);

            if(userProfile == null || prescriber == null || prescriberProfile == null)
                return Failure(404, "There does not appear to be a prescriber associate with your account.");

            prescriberProfile.AddressID = userProfile.PrimaryAddressID ?? 0;
            prescriberProfile.ContactID = userProfile.PrimaryContactID ?? 0;
            prescriberProfile.PrescriberID = prescriber.ID;

            prescriberProfile.Save();

            return new ReturnObject
            {
                Result = null,
                Redirect = new ReturnRedirectObject
                {
                    //Hash = "dashboard"
                    Url = "Default.aspx#dashboard"
                },
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject 
                    {
                        text = "The profile has been attached to your account.",
                        title = "Profile Updated"
                    }
                }
            };
        }

        [Method("Prescriber/Wizards/FirstLogin")]
        public static ReturnObject Update(HttpContext context, long id, long facility_id, string agree_to_terms, string new_password, string confirm_password, string watched_video, 
            string prescriber_type, long prescriber_speciality, string npi,
            string first_name, string last_name, string title, string email, string phone, string fax,
            string street_1, string city, string state, long issuer, string zip, string country, 
            string prefix = null, string postfix = null, string street_2 = null, string state_id = null)
        {
            // load the profile we're finishing
            PrescriberProfile profile = new PrescriberProfile(id);

            // save the contact
            Contact contact = new Contact()
            {
                Prefix = prefix,
                FirstName = first_name,
                LastName = last_name,
                Postfix = postfix,
                Email = email,
                Phone = phone,
                Fax = fax,
                Title = title
            };
            contact.Save();

            // save the address
            Address address = new Address()
            {
                Street1 = street_1,
                Street2 = street_2,
                City = city,
                State = state,
                Country = country,
                Zip = zip
            };
            address.Save();

            profile.PrimaryFacilityID = facility_id;

            // get the prescriber type
            PrescriberType type = PrescriberType.FindByDisplayName(prescriber_type);

            if(type != null)
                profile.PrescriberTypeID = type.ID;

            profile.Save();

            // see if the prescriber is already in the system
            Lib.Data.Prescriber prescriber = Lib.Data.Prescriber.FindByStateId(issuer, state_id);

            if(prescriber != null)
            {
                // tie the new profile to the existing prescriber
                profile.PrescriberID = prescriber.ID;
                profile.Save();

                // login the existing user so they don't get bounced to the login page.
                Framework.Security.Manager.Login(prescriber.Profile.User);

                return new ReturnObject
                {
                    Result = null,
                    Redirect = new ReturnRedirectObject
                    {
                        //Hash = "dashboard"
                        Url = "Default.aspx#dashboard"
                    },
                    Growl = new ReturnGrowlObject
                    {
                        Type = "default",
                        Vars = new ReturnGrowlVarsObject 
                        {
                            text = "The profile has been attached to your account.",
                            title = "Profile Updated"
                        }
                    }
                };
            }

            // create the new prescriber
            String error;
            User user = Framework.Security.Manager.CreateUser(contact.FirstName.Substring(0,1)+contact.LastName, new_password, email, out error);
            user.Save();
            
            Group g1 = new Group(2);
            Group g2 = new Group(3);

            user.AddGroup(g1);
            user.AddGroup(g2);

            UserProfile userProfile = new UserProfile()
            {
                PrimaryAddressID = address.ID,
                PrimaryContactID = contact.ID,
                Created = DateTime.Now,
                UserID = user.ID ?? 0,
                UserTypeID = 3
            };
            userProfile.Save();

            prescriber = new Data.Prescriber
            {
                NpiId = npi,
                StateId = state_id,
                StateIdIssuer = issuer,
                ProfileID = userProfile.ID,
                SpecialityID = prescriber_speciality == 0 ? (long?)null : prescriber_speciality
            };
            prescriber.Save();

            // set the prescriber id
            profile.PrescriberID = prescriber.ID;
            profile.Save();

            // setup the default user peferences
            UserPreferences prefs = new UserPreferences
            {
                UserId = user.ID ?? 0,
                EmailNotifications = true
            };
            prefs.Save();

            Framework.Security.Manager.Login(user);

            //prescriber.

            return Success(
                "Profile Updated", 
                "Your profile has been updated.", 
                null,
                "Locked.aspx#prescriber/wizards/etasu-selections");
        }

        [Method("Prescriber/Wizards/ETASUSelections")]
        public static ReturnObject ETASUSelections(HttpContext context, Dictionary<string, object> drug_selection)
        {
            // THIS IMPLELEMTATION WILL NOT WORK LONG TERM.  CHECKBOXES = BAD
            try
            {
                var userProfile = Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
                var prescriber = Data.Prescriber.FindByProfile(userProfile);

                // process each drug
                foreach(string key in drug_selection.Keys)
                {
                    long id = long.Parse(key);
                    Lib.Data.Drug drug = new Lib.Data.Drug(id);

                    if(!drug.Active)
                        throw new ArgumentException("That is not a valid drug", "drug_selections");

                    // record the selected value
                    var drugSelection = new DrugSelection
                    {
                        PrescriberID = prescriber.ID ?? 0,
                        DrugID = drug.ID ?? 0,
                        DateRecorded = DateTime.Now,
                        Prescribes = ((string)drug_selection[key]).ToLower() == "yes"
                    };

                    drugSelection.Save();

                    // now add the drug to the prescriber
                    if(drugSelection.Prescribes)
                        Drug.AddDrugToPrescriber(drug.ID ?? 0, 9999);
                }
            }
            catch(FormatException)
            {
                return Failure(404, "The selected drug does not exist in the system.");
            }
            catch(ArgumentException ex)
            {
                return Failure(404, ex.Message);
            }
            catch(SecurityException ex)
            {
                return Failure(403, ex.Message);
            }

            return Success(
                "Drugs Updated",
                "Your drug selections have been updated.",
                "prescriber/wizards/non-etasu-selections");
        }

        [Method("Prescriber/Wizards/NonETASUSelections")]
        public static ReturnObject NonETASUSelections(HttpContext context, string[] drug_selections)
        {
            User user = Framework.Security.Manager.GetUser();

            // NonETASU seleciton is opitonal.  For this reason checkboxes are used and not radio
            // buttons.  Any drug in drug_selections has been selected by the user.
            try
            {
                var userProfile = Data.UserProfile.FindByUser(user);
                var prescriber = Data.Prescriber.FindByProfile(userProfile);

                // process each drug
                foreach(string selection in drug_selections)
                {
                    Lib.Data.Drug drug = new Lib.Data.Drug(long.Parse(selection));

                    if(!drug.Active)
                        throw new ArgumentException("That is not a valid drug", "drug_selections");

                    // record the "yes" drug selection
                    var drugSelection = new DrugSelection
                    {
                        PrescriberID = prescriber.ID ?? 0,
                        DrugID = drug.ID ?? 0,
                        DateRecorded = DateTime.Now,
                        Prescribes = true
                    };

                    drugSelection.Save();

                    // now add the drug to the prescriber
                    Drug.AddDrugToPrescriber(drug.ID ?? 0, 9999);
                }

                SetAllRemainingDrugsToNo();
            }
            catch(FormatException)
            {
                return Failure(404, "The selected drug does not exist in the system.");
            }
            catch(ArgumentException ex)
            {
                return Failure(404, ex.Message);
            }
            catch(SecurityException ex)
            {
                return Failure(403, ex.Message);
            }

            NotificationService.Send(
                NotificationService.Create("Profile Complete", "You have successfully completed filling out your profile", false), 
                user
                );

            return Success(
                "Drugs Updated",
                "Your drug selections have been updated.",
                null,
                "Default.aspx#prescriber/drugs/list");
        }

        [Method("Prescriber/Wizards/LastStep")]
        public static ReturnObject LastStep(HttpContext context, int email_frequency, bool email_notifications = false)
        {
            return Success(
                "Preferences Saved",
                "You have successfully updated your preferences.",
                null,
                "Default.aspx#dashboard");
        }

        #region Utility Methods
        private static ReturnObject Success(string title, string text, string hash, string url = null)
        {
            return new ReturnObject
            {
                Error = false,
                StatusCode = 200,
                Redirect = new ReturnRedirectObject
                {
                    Hash = hash,
                    Url = url
                },
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = text,
                        title = title
                    }
                }
            };
        }

        private static ReturnObject Failure(int statusCode, string message)
        {
            return new ReturnObject
            {
                Error = true,
                StatusCode = statusCode,
                Message = message
            };
        }

        private static void SetAllRemainingDrugsToNo()
        {
            // After the ETASU selection screen and the non-ETASU selection screen,
            // the only drugs left with no selection would be the non-ETASU drugs
            // the user did not check on the non-ETASU selection page.  Let's set
            // those all to "no".
            IList<Lib.Data.Drug> drugs = Lib.Systems.Drugs.GetDrugsWithNoSelection(Framework.Security.Manager.GetUser());

            var userProfile = Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
            var prescriber = Data.Prescriber.FindByProfile(userProfile);

            foreach(Lib.Data.Drug drug in drugs)
            {
                // record the "no" drug selection
                DrugSelection drugSelection = new DrugSelection
                {
                    PrescriberID = prescriber.ID ?? 0,
                    DrugID = drug.ID ?? 0,
                    DateRecorded = DateTime.Now,
                    Prescribes = false
                };

                drugSelection.Save();
            }
        }
        #endregion
    }
}