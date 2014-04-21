using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemsLogic.Model
{
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
