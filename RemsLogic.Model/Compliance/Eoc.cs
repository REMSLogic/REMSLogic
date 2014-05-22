using System.Collections.Generic;

namespace RemsLogic.Model.Compliance
{
    public class Eoc : Entity
    {
        public string Name {get; set;}
        public string DisplayName {get; set;}
        public List<string> AppliesTo {get; set;}
        public string ShortDisplayName {get; set;}
        public string LargeIcon {get; set;}
        public string SmallIcon {get; set;}
    }
}
