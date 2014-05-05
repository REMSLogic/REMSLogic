using System.Collections.Generic;
using System.Linq;
using RemsLogic.Model.Compliance;

namespace RemsLogic.Repositories.ProxyObjects
{
    public class PrescriberEocProxy : PrescriberEoc
    {
        private readonly IComplianceRepository _complianceRepo;

        public PrescriberEocProxy(IComplianceRepository complianceRepo)
        {
            _complianceRepo = complianceRepo;
        }

        public override List<PrescriberEocLogEntry> ComplianceLog
        {
            get{return base.ComplianceLog ?? (base.ComplianceLog = LoadComplianceLog());}
        }

        private List<PrescriberEocLogEntry> LoadComplianceLog()
        {
            // handle the lazzy loading of the navigation property
            return _complianceRepo.GetComplianceLog(Id).ToList();
        }
    }
}
