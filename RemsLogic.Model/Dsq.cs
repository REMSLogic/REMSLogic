using System.Collections.Generic;

namespace RemsLogic.Model
{
    /// <summary>
    /// Currently this class is nothing more than a stub / place holder.  Ideally
    /// this would serve as the aggregate root in a more DDD approach.
    /// </summary>
    public class Dsq : Entity
    {
        public virtual IEnumerable<DsqQuestion> Questions {get; set;}
        public virtual IEnumerable<DsqAnswer> Answers {get; set;}
        public virtual IEnumerable<DsqLink> Links {get; set;}
    }
}
