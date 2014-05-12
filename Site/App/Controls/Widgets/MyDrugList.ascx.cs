using System;
using System.Text;
using RemsLogic.Model;
using RemsLogic.Model.Compliance;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Controls.Widgets
{
    public partial class MyDrugList : System.Web.UI.UserControl
    {
        private readonly IComplianceService _complianceSvc;
        private readonly IDrugListService _drugListSvc;

        public MyDrugList()
        {
            _complianceSvc = ObjectFactory.GetInstance<IComplianceService>();
            _drugListSvc = ObjectFactory.GetInstance<IDrugListService>();
        }

        public DrugList GetDrugList()
        {
            return _drugListSvc.GetDrugListByProfileId(
                Lib.Systems.Security.GetCurrentProfile().ID.Value, 
                DrugListType.Favorites);
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
    }
}