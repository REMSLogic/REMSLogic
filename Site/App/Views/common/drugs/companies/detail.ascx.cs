using System;
using System.Collections.Generic;
using Lib.Web;
using Lib.Data;

namespace Site.App.Views.common.drugs.companies
{
    public partial class detail : AppControlPage
    {
        #region Properties
        public DrugCompany DrugCompany {get; set;}
        public IList<DrugCompanyUser> Users {get; set;}
        public DrugFormulation Formulation {get; set;}
        #endregion

        #region Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            string strId = Request.QueryString["id"];
            string strFormulationId = Request.QueryString["formulationId"];
            long id;
            long formulationId;

            if(String.IsNullOrEmpty(strId) || !long.TryParse(strId, out id))
            {
                DrugCompany = new DrugCompany();
                Users = new List<DrugCompanyUser>();
            }
            else
            {
                DrugCompany = new DrugCompany(id);
                //Users = DrugCompanyUser.FindByDrugCompany(DrugCompany);
                Users = new List<DrugCompanyUser>();
            }

            if(!String.IsNullOrEmpty(strFormulationId) && 
                long.TryParse(strFormulationId, out formulationId))
            {
                if(formulationId > 0)
                    Formulation = new DrugFormulation(formulationId);
            }
        }
        #endregion
    }
}