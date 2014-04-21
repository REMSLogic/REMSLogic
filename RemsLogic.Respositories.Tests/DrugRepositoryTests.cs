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
    public class DrugRepositoryTests
    {
        private IDrugRepository _drugRepo;
        private string _connectionString;

        [SetUp]
        public void InitializeValues()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            _drugRepo = new DrugRepository(_connectionString);
        }

        [Test]
        public void should_load_drug_19()
        {
            // arrange
            const long drugId = 19;
            Drug drug = null;

            // act
            drug = _drugRepo.Get(drugId);

            // assert
            drug.Should().NotBeNull();
            drug.GenericName.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void should_load_all_drugs_for_prescriber_profile()
        {
            // arrange
            const long profileId = 1;
            List<Drug> drugs = null;

            // act
            drugs = _drugRepo.GetByPrescriberProfile(profileId).ToList();

            // assert
            drugs.Should().NotBeNull();
            drugs.Count.Should().NotBe(0);
        }
    }
}
