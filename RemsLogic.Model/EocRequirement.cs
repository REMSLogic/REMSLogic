using System;

namespace RemsLogic.Model
{
    public class EocRequirement : Entity
    {
        public long ProfileId {get; set;}
        public long DrugId {get; set;}
        public long EocId {get; set;}
        public DateTime? CompletedAt {get; set;}
        public bool Deleted {get; set;}
    }
}
