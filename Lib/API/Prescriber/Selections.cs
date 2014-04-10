using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using Framework.API;
using Lib.Data;

namespace Lib.API.Prescriber
{
    public class Selections : Base
    {
        #region API Methods
        [SecurityRole("view_prescriber")]
        [Method("Prescriber/Selections/Edit")]
        public static ReturnObject Edit(HttpContext context, Dictionary<string, object> drug_selection)
        {
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

                    // check for an existing selection (update)
                    var drugSelection = DrugSelection.Find(prescriber.ID ?? 0, drug.ID ?? 0)
                        ?? new DrugSelection
                        {
                            PrescriberID = prescriber.ID ?? 0,
                            DrugID = drug.ID ?? 0
                        };

                    // record the selected value
                    drugSelection.Prescribes = ((string)drug_selection[key]).ToLower() == "yes";
                    drugSelection.DateRecorded = DateTime.Now;

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
                null,
                "Default.aspx#dashboard");
        }
        #endregion

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
        #endregion
    }
}
