using System;
using System.Collections.Generic;
using System.Linq;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Views.admin.dsq
{
    public partial class link : Lib.Web.AdminControlPage
    {
        private readonly IComplianceService _complianceSvc;
        private readonly IDsqService _dsqSvc;

        public DsqLink Link {get; set;}
        //public long SectionID {get; set;}
        public List<Eoc> Eocs {get; set;}
        
        public link()
        {
            _complianceSvc = ObjectFactory.GetInstance<IComplianceService>();
            _dsqSvc = ObjectFactory.GetInstance<IDsqService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            long id = (Request.QueryString["id"] != null)
                ? Int64.Parse(Request.QueryString["id"])
                : 0;
            long drugId = Int64.Parse(Request.QueryString["drug-id"]);
            long questionId = Int64.Parse(Request.QueryString["question-id"]);

            Link = (id > 0 )
                ? _dsqSvc.GetLink(id) 
                : new DsqLink
                    {
                        Date = DateTime.Now,
                        DrugId = drugId,
                        QuestionId = questionId
                    };

            Eocs = _complianceSvc.GetEocs().ToList();
        }
    }
}