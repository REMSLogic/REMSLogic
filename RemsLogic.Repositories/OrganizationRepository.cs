using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RemsLogic.Model;
using RemsLogic.Repositories.ProxyObjects;

namespace RemsLogic.Repositories
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        #region Constructor
        public OrganizationRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region Base Class Implementation
        public override void Save(Organization model)
        {
            if(model.PrimaryFacility == null)
                throw new InvalidOperationException("You must assign a valid primary facility before saving an organization.");

            const string insertSql = @"
                INSERT INTO Organizations
                    (Name, PrimaryFacilityId)
                OUTPUT INSERTED.Id
                VALUES
                    (@Name, @PrimaryFacilityId);";

            const string updateSql = @"
                UPDATE Organizations SET
                    Name = @Name,
                    PrimaryFacilityId = @PrimaryFacilityId
                WHERE
                    Id = @Id;";

            SaveFacility(model.PrimaryFacility);
            model.PrimaryFacilityId = model.PrimaryFacility.Id;

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(model.Id > 0? updateSql : insertSql, connection))
                {
                    command.Parameters.AddRange(new []
                        {
                            new SqlParameter("Name", model.Name),
                            new SqlParameter("PrimaryFacilityId", model.PrimaryFacilityId)
                        });

                    if(model.Id != 0)
                    {
                        command.Parameters.AddWithValue("Id", model.Id);
                        command.ExecuteNonQuery();
                    }
                    else
                        model.Id = (long)command.ExecuteScalar();
                }
            }

            foreach(var facility in model.Facilities)
            {
                facility.OrganizationId = model.Id;
                SaveFacility(facility);
            }
        }

        public override Organization Get(long id)
        {
            const string sql = @"
                SELECT 
                    *
                FROM Organizations
                WHERE Id = @Id;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Id", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read()
                            ? ReadOrganization(reader)
                            : null;
                    }
                }
            }  
        }
        #endregion

        #region IOrganizationRepository Implmentation
        public void SaveAddress(Address model)
        {
            const string insertSql = @"
                INSERT INTO Addresses
                    (Street1, Street2, City, State, Zip, Country)
                OUTPUT INSERTED.Id
                VALUES
                    (@Street1, @Street2, @City, @State, @Zip, @Country);";

            const string updateSql = @"
                UPDATE Addresses SET
                    Street1 = @Street1,
                    Street2 = @Street2,
                    City = @City,
                    State = @State,
                    Zip = @Zip,
                    Country = @Country
                WHERE
                    Id = @Id;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(model.Id > 0? updateSql : insertSql, connection))
                {
                    command.Parameters.AddRange(new []
                        {
                            new SqlParameter("Street1", model.Street1),
                            new SqlParameter("Street2", (object)model.Street2 ?? DBNull.Value),
                            new SqlParameter("City", model.City),
                            new SqlParameter("State", model.State),
                            new SqlParameter("Zip", model.Zip),
                            new SqlParameter("Country", model.Country)

                        });

                    if(model.Id != 0)
                    {
                        command.Parameters.AddWithValue("Id", model.Id);
                        command.ExecuteNonQuery();
                    }
                    else
                        model.Id = (long)command.ExecuteScalar();
                }
            }   
        }


        public Address GetAddress(long addressId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM Addresses
                WHERE Id = @Id;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Id", addressId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read()
                            ? ReadAddress(reader)
                            : null;
                    }
                }
            }  
        }

        public void SaveFacility(Facility model)
        {
            if(model.Address == null)
                throw new InvalidOperationException("You must assign a valid address before saving a facility.");

            const string insertSql = @"
                INSERT INTO Facilities
                    (AddressId, Name, BedSize, OrganizationId)
                OUTPUT INSERTED.Id
                VALUES
                    (@AddressId, @Name, @BedSize, @OrganizationId);";

            const string updateSql = @"
                UPDATE Facilities SET
                    AddressId = @AddressId,
                    Name = @Name,
                    BedSize = @BedSize,
                    OrganizationId = @OrganizationId
                WHERE
                    Id = @Id;";

            SaveAddress(model.Address);
            model.AddressId = model.Address.Id;

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(model.Id > 0? updateSql : insertSql, connection))
                {
                    command.Parameters.AddRange(new []
                        {
                            new SqlParameter("AddressId", model.Address.Id),
                            new SqlParameter("Name", model.Name),
                            new SqlParameter("BedSize", model.BedSize),
                            new SqlParameter("OrganizationId", model.OrganizationId)
                        });

                    if(model.Id != 0)
                    {
                        command.Parameters.AddWithValue("Id", model.Id);
                        command.ExecuteNonQuery();
                    }
                    else
                        model.Id = (long)command.ExecuteScalar();
                }
            }   
        }

        public Facility GetFacility(long facilityId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM Facilities
                WHERE Id = @Id;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Id", facilityId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read()
                            ? ReadFacility(reader)
                            : null;
                    }
                }
            }  
        }

        public IEnumerable<Facility> GetOrganizationFacilities(long organizationId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM Facilities
                WHERE OrganizationId = @OrgId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("OrgId", organizationId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return ReadFacility(reader);
                        }
                    }
                }
            }  
        }
        #endregion

        #region Utility Methods
        private Address ReadAddress(SqlDataReader reader)
        {
            return new Address
            {
                Street1 = (string)reader["Street1"],
                Street2 = (reader["Street2"] != DBNull.Value)
                    ? (string)reader["Street2"]
                    : null,
                City = (string)reader["City"],
                State = (string)reader["State"],
                Zip = (string)reader["Zip"],
                Country = (string)reader["Country"]
            };
        }

        private Organization ReadOrganization(SqlDataReader reader)
        {
            return new OrganizationProxy(this)
            {
                Id = (long)reader["Id"],
                Name = (string)reader["Name"],
                PrimaryFacilityId = (long)reader["PrimaryFacilityId"]
            };
        }

        private Facility ReadFacility(SqlDataReader reader)
        {
            return new FacilityProxy(this)
            {
                Id = (long)reader["Id"],
                AddressId = (long)reader["AddressId"],
                Name = (string)reader["Name"],
                BedSize = (string)reader["BedSize"],
                OrganizationId = (long)reader["OrganizationId"]
            };
        }
        #endregion
    }
}
