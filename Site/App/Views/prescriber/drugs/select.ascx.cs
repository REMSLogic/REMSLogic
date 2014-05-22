using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemsLogic.Model.Compliance;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Views.prescriber.drugs
{
    public partial class select : Lib.Web.AppControlPage
    {
        private readonly IComplianceService _complianceSvc;

        public IList<Lib.Data.Drug> Drugs;
        public IList<Lib.Data.Drug> AvailableDrugs;
        public IList<Lib.Data.Drug> SelectedDrugs;
        public List<Eoc> Eocs {get; set;}

        public select()
        {
            _complianceSvc = ObjectFactory.GetInstance<IComplianceService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Drugs = Lib.Data.Drug.FindAll();
            SelectedDrugs = Lib.Systems.Lists.GetMyDrugs();
            AvailableDrugs = new List<Lib.Data.Drug>();

            foreach (var d in Drugs)
            {
                bool found = false;
                for (int i = 0; i < SelectedDrugs.Count; i++)
                {
                    if (d.ID.Value == SelectedDrugs[i].ID.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    continue;

                AvailableDrugs.Add(d);
            }
            SelectedDrugs = SelectedDrugs.OrderBy(l => l.GenericName).ToList();
            AvailableDrugs = AvailableDrugs.OrderBy(l => l.GenericName).ToList();
            Eocs = _complianceSvc.GetEocs().ToList();
        }

        public string GetEOCData(Lib.Data.Drug d)
        {
            StringBuilder eocData = new StringBuilder();

            foreach(Eoc eoc in _complianceSvc.GetByDrug(d.ID ?? 0))
            {
                eocData.Append(String.Format("data-{0}=\"1\" ", eoc.Name));
            }

            return eocData.ToString();
        }

        #region Utilty Methods
        public bool DisplayEoc(Eoc eoc)
        {
            return eoc.AppliesTo.Any(role => Framework.Security.Manager.HasRole(role));
        }
        #endregion
    }
}