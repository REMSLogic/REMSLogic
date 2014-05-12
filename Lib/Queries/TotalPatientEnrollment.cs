using System.Collections.Generic;
using Framework.Data;

namespace Lib.Queries
{
    public class TotalPatientEnrollment
    {
        public int Run(long facilityId)
        {
            var db = Database.Get("FDARems");

            const string sql = @"
                SELECT TOP 1
                    COUNT(DISTINCT PrescriberProfiles.PrescriberID)
                FROM UserEocs
                    INNER JOIN Prescribers ON UserEocs.ProfileID = Prescribers.ProfileID
                    INNER JOIN PrescriberProfiles ON PrescriberProfiles.PrescriberID = Prescribers.ID
                WHERE
                    PrescriberProfiles.PrimaryFacilityID = @FacilityId AND
                    UserEocs.EocID = 2;";

            var parameters = new List<Parameter>()
            {
                new Parameter("FacilityId", facilityId)
            };

            return db.ExecuteScalar<int>(sql, parameters.ToArray());
        }
    }
}
