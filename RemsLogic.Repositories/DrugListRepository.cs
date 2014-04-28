using RemsLogic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RemsLogic.Repositories
{
    public class DrugListRepository : Repository<DrugList>, IDrugListRepository
    {

        #region Constructor

        public DrugListRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IDrugListRepository Implementation

        public long GetDrugListId(long profileId, string listType)
        {
            if(String.IsNullOrEmpty(listType))
                throw new ArgumentException("Invalid list ltype", "listType");

            const string SQL = "SELECT TOP 1 * FROM [dbo].[UserLists] WHERE UserProfileID = @ProfileId AND Name = @Name";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@ProfileId", profileId);
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 500));
                    cmd.Parameters["@Name"].Value = listType;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return (long)reader["ID"];
                    }
                }
            }

            return 0;
        }

        public List<long> GetFavList(long profileId)
        {
            List<long> retList = new List<long>();
            const string sql = @"
                SELECT Drugs.ID
                FROM UserListItems
	                INNER JOIN UserLists ON UserLists.ID = UserListItems.ListID
	                INNer JOIN Drugs ON Drugs.ID = UserListItems.ItemID
                WHERE
	                DataType = 'drug' AND
	                UserProfileID = @ProfileId AND
                    Name = 'Fav Drugs'
                ORDER BY
	                GenericName ASC;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", profileId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retList.Add((long)reader["ID"]);
                        }
                    }
                }
            }

            return retList;
        }

        public void AddDrugToList(long profileId, long drugId, string listType)
        {
            long listId = GetOrCreateNewDrugListByProfileId(profileId, listType);

            string SQL = "INSERT INTO [dbo].[UserListItems]([ListID],[ItemID],[Order],[DateAdded]) " +
                         "VALUES(@ListId,@ItemId,0,@Date)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@ListId", listId);
                    cmd.Parameters.AddWithValue("@ItemId", drugId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    connection.Open();
                    cmd.ExecuteScalar();
                }
            }
        }

        public void RemoveDrugFromList(long profileId, long drugId, string listType)
        {
            long listId = GetDrugListId(profileId, listType);

            if (listId != 0)
            {
                string SQL = "DELETE FROM [dbo].[UserListItems] WHERE ListID = @ListId AND ItemID = @ItemId";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(SQL, connection))
                    {
                        cmd.Parameters.AddWithValue("@ListId", listId);
                        cmd.Parameters.AddWithValue("@ItemId", drugId);
                        connection.Open();
                        cmd.ExecuteScalar();
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private long GetOrCreateNewDrugListByProfileId(long profileId, string listType)
        {
            // if GetDRugListId returned long?, then this method could be simplified to
            // 
            // return GetDrugListId(profileId, listType) ?? CreateNewList(listType, profileId);

            long listId = GetDrugListId(profileId, listType);

            if (listId == 0)
            {
                listId = CreateNewList(profileId, listType);
            }

            return listId;
        }

        private long CreateNewList(long profileId, string listType)
        {
            if(String.IsNullOrEmpty(listType))
                throw new ArgumentException("You must specify a valid list type.", "listType");

            const string SQL = "INSERT INTO [dbo].[UserLists]( [UserProfileID],[Name],[DateCreated],[DateModified],[DataType],[System]) " +
                            "VALUES( @ProfileId,@Name,@Date,@Date,'drug',1); " +
                            "SELECT ID FROM [dbo].[UserLists] WHERE [UserProfileID] = @ProfileId AND [Name] = @Name";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@ProfileId", profileId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    // why the different approach for this parameter?
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 500));
                    cmd.Parameters["@Name"].Value = listType;

                    connection.Open();

                    return (long)cmd.ExecuteScalar();
                }
            }
        }
        #endregion
    }
}
