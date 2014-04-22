using System.Collections.Generic;

namespace RemsLogic.Model
{
    public class Eoc : Entity
    {
        public string Name {get; set;}
        public string DisplayName {get; set;}
        public List<string> AppliesTo {get; set;}
    }
}
