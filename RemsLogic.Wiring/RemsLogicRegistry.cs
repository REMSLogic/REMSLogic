using System.Configuration;
using RemsLogic.Repositories;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;

namespace RemsLogic.Wiring
{
    public class RemsLogicRegistry : Registry
    {
        public RemsLogicRegistry()
        {
            Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                    x.AssembliesFromApplicationBaseDirectory(f => f.FullName.Contains("RemsLogic"));
                });

            string connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;

            For<IComplianceRepository>().Use(c => new ComplianceRepository(connectionString));
            For<IDrugListRepository>().Use(c => new DrugListRepository(connectionString));
            For<IDrugRepository>().Use(c => new DrugRepository(connectionString));
            For<IWidgetRepository>().Use(c => new WidgetRepository(connectionString));
            For<IDsqRepository>().Use(c => new DsqRepository(connectionString));
        }
    }
}
