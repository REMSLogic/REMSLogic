using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemsLogic.Model;
using RemsLogic.Model.Compliance;
using RemsLogic.Services;
using System;
using StructureMap;

namespace Site.App.Views.prescriber.drugs
{
    public partial class list : Lib.Web.AppControlPage
    {
        private readonly IDrugListService _drugListSvc;
        private readonly IComplianceService _complianceSvc;

        public DrugList Drugs;
        public List<Eoc> Eocs {get; set;}

        public list()
        {
            _drugListSvc = ObjectFactory.GetInstance<IDrugListService>();
            _complianceSvc = ObjectFactory.GetInstance<IComplianceService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Drugs = Lib.Systems.Lists.GetMyDrugs();
            long profileId = Lib.Systems.Security.GetCurrentProfile().ID.Value;
            Drugs = _drugListSvc.GetDrugListByProfileId(profileId, DrugListType.MyDrugs);
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