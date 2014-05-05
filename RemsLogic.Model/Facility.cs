using System.Collections.Generic;

namespace RemsLogic.Model
{
    public class Facility : Entity
    {
        public long OrganizationId {get; set;}
        public long AddressId {get; set;}

        public string Name {get; set;}
        public string BedSize {get; set; }

        #region Navigation Properties
        //public virtual Organization Organization {get; set;}
        public virtual Address Address {get; set;}
        public virtual List<User> Users {get; set;}
        public virtual List<Prescriber> Prescribers {get; set;}
        #endregion
    }
}
