using System.Collections.Generic;

namespace RemsLogic.Model
{
    public static class DrugListType
    {
        public const string Undefined = "Undefined";
        public const string MyDrugs = "My Drugs";
        public const string Favorites = "Fav Drugs";
    }

    public class DrugList : Entity
    {
        public string ListName { get; set; }
        public long UserProfileId { get; set; }
        public List<DrugListItem> Drugs { get; set; }

        public DrugList()
        {
            Drugs = new List<DrugListItem>();
        }
    }
}
