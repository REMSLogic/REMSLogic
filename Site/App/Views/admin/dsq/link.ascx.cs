using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib.Data.DSQ;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Views.admin.dsq
{
    public partial class link : Lib.Web.AdminControlPage
    {
        private readonly IComplianceService _complianceSvc;

        public Link item {get; set;}
        public long SectionID {get; set;}
        public List<Eoc> Eocs {get; set;}

        
        public link()
        {
            _complianceSvc = ObjectFactory.GetInstance<IComplianceService>();

            SectionID = -1;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string strId = Request.QueryString["id"];
            string strDrugId = Request.QueryString["drug-id"];
            string strQuestionId = Request.QueryString["question-id"];

            long id;
            long drugId;
            long questionId;

            item = (String.IsNullOrEmpty(strId) || long.TryParse(strId, out id))
                ? new Link()
                : new Link(id);

            if (!string.IsNullOrEmpty(strDrugId) && long.TryParse(strDrugId, out drugId))
                item.DrugID = drugId;
            else
            {
                RedirectHash("admin/drugs/drugs/list", true, "No drug selected to add a link to.");
                return;
            }

            if (!string.IsNullOrEmpty(strQuestionId) && long.TryParse(strQuestionId, out questionId))
                item.QuestionID = questionId;
            else
            {
                RedirectHash("admin/dsq/edit?id="+item.DrugID, true, "No question selected to add a link to.");
                return;
            }

            if (item.QuestionID == 0)
                return;

            Eocs = _complianceSvc.GetByDrug(drugId).ToList();

            var q = new Question(item.QuestionID);
            SectionID = q.SectionID;
        }
    }
}