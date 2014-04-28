using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemsLogic.Model
{
    public class DrugListItem : Entity
    {
        public string DrugName { get; set; }

        public DateTime DateAdded { get; set; }

        public int DrugEocsCount { get; set; }

        public int UserEocsCount { get; set; }

        public bool IsFav { get; set; }

        public float PercentComplete
        {
            get
            {
                if (DrugEocsCount <= 0)
                    return 1.0f;
                if (UserEocsCount <= 0)
                    return 0.0f;

                return ((float)UserEocsCount) / ((float)DrugEocsCount);
            }
        }
    }
}
