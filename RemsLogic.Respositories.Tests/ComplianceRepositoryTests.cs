﻿using System.Collections.Generic;
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
    }
}
