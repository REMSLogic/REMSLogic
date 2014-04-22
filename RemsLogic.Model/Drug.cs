using System;

namespace RemsLogic.Model
{
    public class Drug : Entity
    {
        public long ClassId {get; set;}
        public long? SystemId {get; set;}
        public long UpdatedById {get; set;}
        public string GenericName {get; set;}
        public string RemsReason {get; set;}
        public string Indication {get; set;}
        public string RemsProgramUrl {get; set;}
        public string FdaApplicationNumber {get; set;}
        public DateTime? RemsApproved {get; set;}
        public DateTime? RemsUpdated {get; set;}
        public DateTime Updated {get; set;}
        public bool Active {get; set;}
        public string EocIcons {get; set;}
    }
}
