using System;

namespace RemsLogic.Model.Ecommerce
{
    public class Account : Entity
    {
        public long UserProifleId {get; set;}
        public DateTime CreatedAt {get ;set;}
        public DateTime ExpiresOn {get; set;}
        public bool IsEnabled {get; set;}
    }
}
