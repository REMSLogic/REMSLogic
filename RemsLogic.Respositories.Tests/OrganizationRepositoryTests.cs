using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace RemsLogic.Respositories.Tests
{
    [TestFixture]
    public class OrganizationRepositoryTests
    {
        string _connectionString;
        IOrganizationRepository _orgRepo;

        Organization _org;
        List<Facility> _facilities;

        [SetUp]
        public void InitTestObjects()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            _orgRepo = new OrganizationRepository(_connectionString);
            
            _facilities = new List<Facility>
            {
                new Facility
                {
                    Id = 0,
                    Name = "Facility 1",
                    BedSize = "Big-ish",
                    Address = new Address
                    {
                        Street1 = "Facility 1 - Street 1",
                        City = "City",
                        State = "State",
                        Zip = "Zip",
                        Country = "Country"
                    }
                },
                new Facility
                {
                    Id = 0,
                    Name = "Facility 2",
                    BedSize = "Small-ish",
                    Address = new Address
                    {
                        Street1 = "Facility 2 - Street 1",
                        City = "City",
                        State = "State",
                        Zip = "Zip",
                        Country = "Country"
                    }
                }
            };

            _org = new Organization
            {
                Id = 0,
                Name = "Akron General Hospitoal",
                PrimaryFacility = _facilities[0],
                Facilities = _facilities
            };
        }

        [Test, Ignore]
        public void should_save_new_organization_to_repository()
        {
            // arrange 
            _org.Id = 0;

            // act
            _orgRepo.Save(_org);

            // assert
            _org.Id.Should().BeGreaterThan(0);
            _org.PrimaryFacilityId.Should().BeGreaterThan(0);
            _org.PrimaryFacility.AddressId.Should().BeGreaterThan(0);
        }

        [Test, Ignore]
        public void should_load_organization_add_lazy_load_nav_properties()
        {
            // arrange
            const long orgId = 5;

            Organization org;

            // act
            org = _orgRepo.Get(orgId);

            // assert
            org.Should().NotBeNull();
            org.PrimaryFacility.Should().NotBeNull();
            org.PrimaryFacility.Address.Should().NotBeNull();
            org.Facilities.Count.Should().BeGreaterThan(0);
        }

        [Test, Ignore]
        public void should_update_existing_organization()
        {
            // arrange
            string expectedName = String.Format("Test {0}", DateTime.Now.Ticks);

            _org.Id = 1;
            _orgRepo.Save(_org);

            // act
            var loadedOrg = _orgRepo.Get(1);

            loadedOrg.Name = expectedName;
            _orgRepo.Save(loadedOrg);
            loadedOrg = _orgRepo.Get(1);

            // assert
            loadedOrg.Name.Should().Be(expectedName);
        }
    }
}
