using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace RemsLogic.Respositories.Tests
{
    [TestFixture]
    public class ComplianceRepositoryTests
    {
        private IComplianceRepository _complianceRepo;
        private string _connectionString;

        [SetUp]
        public void InitializeValues()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            _complianceRepo = new ComplianceRepository(_connectionString);
        }

        [Test]
        public void should_read_eoc_for_drug_19()
        {
            // arrange
            long drugId = 19;
            List<Eoc> eocs = null;

            // act
            eocs = _complianceRepo.GetByDrugId(drugId).ToList();

            // assert
            eocs.Should().NotBeNull();
            eocs.Count.Should().NotBe(0);
        }

        [Test, Ignore("I don't want this run against dev or prod databases.")]
        public void should_save_and_read_prescriber_eoc()
        {
            // arrange
            PrescriberEoc savedEoc = new PrescriberEoc
            {
                PrescriberProfileId =  -1,
                DrugId = -1,
                EocId = -1,
                Deleted = true,
                CompletedAt = null
            };

            PrescriberEoc loadedEoc = null;

            // act
            _complianceRepo.Save(savedEoc);
            loadedEoc = _complianceRepo.Find(-1, -1, -1);
            
            loadedEoc.Deleted = false;
            _complianceRepo.Save(loadedEoc);
            loadedEoc = _complianceRepo.Find(-1, -1, -1);

            // assert
            loadedEoc.Should().NotBeNull();
            loadedEoc.Id.Should().NotBe(0);
            loadedEoc.DrugId.Should().Be(-1);
            loadedEoc.CompletedAt.Should().Be(null);
            loadedEoc.Deleted.Should().BeFalse();
        }
    }
}
