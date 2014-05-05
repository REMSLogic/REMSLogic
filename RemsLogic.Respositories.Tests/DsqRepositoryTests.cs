using System;
using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace RemsLogic.Respositories.Tests
{
    [TestFixture]
    public class DsqRepositoryTests
    {
        public IDsqRepository _dsqRepo;
        private string _connectionString;

        [SetUp]
        public void Init()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            _dsqRepo = new DsqRepository(_connectionString);
        }

        [Test, Ignore()]
        public void should_insert_new_dsq_link_into_db()
        {
            // arrange
            DsqLink link = new DsqLink
            {
                Date = DateTime.Now,
                DrugId = 1,
                EocId = 1,
                HelpText = "Help Text",
                Label = "Label",
                QuestionId = 1,
                Value = "Test"
            };

            // act
            _dsqRepo.SaveLink(link);

            // assert
            link.Id.Should().NotBe(0);
        }
    }
}
