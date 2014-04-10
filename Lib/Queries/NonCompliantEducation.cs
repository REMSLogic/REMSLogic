using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Data;

namespace Lib.Queries
{
    public class NonCompliantEducation
    {
        public int Run(long providerId)
        {
            var db = Database.Get("FDARems");

            string sql = @"
                SELECT TOP 1
                    COUNT(*) OVER() AS 'Count'
                FROM ((((((((Prescribers 
                    INNER JOIN PrescriberProfiles ON Prescribers.ProfileID = PrescriberProfiles.ID)
                    INNER JOIN UserProfiles ON UserProfiles.ID = PrescriberProfiles.ID)
                    INNER JOIN Contacts ON Contacts.ID = PrescriberProfiles.ContactID)
                    LEFT JOIN PrescriberTypes ON PrescriberTypes.ID = PrescriberProfiles.PrescriberTypeID)
                    LEFT JOIN Specialities ON Specialities.ID = Prescribers.SpecialityID)
                    LEFT JOIN ProviderFacilities ON ProviderFacilities.ID = PrescriberProfiles.PrimaryFacilityID)
                    LEFT JOIN UserLists ON UserLists.UserProfileID = UserProfiles.ID)
                    LEFT JOIN UserListItems ON UserListItems.ListID = UserLists.ID)
                    LEFT JOIN Drugs ON Drugs.ID = UserListItems.ItemID
                    LEFT JOIN UserEocs ON UserEocs.DrugID = Drugs.ID AND UserEocs.EocID = 4
                WHERE
                    UserLists.Name = 'My Drugs' AND
                    UserEocs.DateCompleted IS NULL AND
                    PrescriberProfiles.PrescriberId IS NOT NULL AND
                    PrescriberProfiles.Deleted = 0 AND
                    PrescriberProfiles.ProviderID = @ProviderId
                GROUP BY
                    PrescriberProfiles.PrescriberID;";

            var parameters = new List<Parameter>()
            {
                new Parameter("ProviderId", providerId)
            };

            return db.ExecuteScalar<int>(sql, parameters.ToArray());
        }
    }
}
