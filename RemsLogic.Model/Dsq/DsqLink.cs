using System;

namespace RemsLogic.Model.Dsq
{
    public class DsqLink : Entity
    {
        public long QuestionId {get; set;}
        public long DrugId {get; set;}
        public long EocId {get; set;}
        public bool IsRequired {get; set;}
        public string Label {get; set;}
        public string Value {get; set;}
        public string HelpText {get; set;}
        public DateTime? Date {get; set;}

        public virtual DsqQuestion Question {get; set;}
    }
}
