using System;

namespace RemsLogic.Model
{
    public class RestrictedLink : Entity
    {
        public string Url {get; set;}
        public Guid Token {get; set;}
        public DateTime ExpirationDate {get; set;}
        public string CreatedFor {get; set;}
    }
}
