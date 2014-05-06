using System.Collections.Generic;

namespace RemsLogic.Model
{
    public class Organization : Entity
    {
        public long PrimaryFacilityId {get; set;}
        public string Name {get; set; }

        #region Navigation Properties
        public virtual Facility PrimaryFacility {get; set;}
        public virtual List<Facility> Facilities {get; set;}
        #endregion
    }
}
