using System;

namespace RemsLogic.Model.Compliance
{
    public class PrescriberEocLogEntry : Entity
    {
        public long PrescriberEocId {get; set;}
        public DateTime RecordedAt {get; set;}
    }
}
