using RemsLogic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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

        public DrugList GetFavoritesListByUserId(long userId)
        {
            DrugList retList = new DrugList();
            long profileId = GetProfileId(userId);

            //Find list Id if exists
            long listId = GetListId(ListType.FavList, profileId);
            if(listId == 0)
            {
                return null;
            }

            retList.Id = listId;
            retList.ListName = "Fav Drugs";
            retList.UserProfileId = profileId;
            retList.Drugs = GetListDrugs(listId, profileId);

            return retList;
        }

        public DrugList GetDrugListByUserId(long userId)
        {
            DrugList retList = new DrugList();
            long profileId = GetProfileId(userId);

            //Find list Id if exists
            long listId = GetListId(ListType.DrugList, profileId);
            if (listId == 0)
            {
                return null;
            }

            retList.Id = listId;
            retList.ListName = "My Drugs";
            retList.UserProfileId = profileId;
            retList.Drugs = GetListDrugs(listId, profileId);

            return retList;
        }

        public bool CreateNewFavoritesList(long userId)
        {
            long profileId = GetProfileId(userId);

            long listId = GetListId(ListType.FavList, profileId);
            if (listId == 0)
            {
                listId = CreateNewList(ListType.FavList, profileId);
                return true;
            }
            return false;
        }

        public bool CreateNewDrugList(long userId)
        {
            long profileId = GetProfileId(userId);

            long listId = GetListId(ListType.DrugList, profileId);
            if (listId == 0)
            {
                listId = CreateNewList(ListType.DrugList, profileId);
                return true;
            }
            return false;
        }

        public void AddDrugToFavorites(long profileId, long drugId)
        {
            long listId = GetListId(ListType.FavList, profileId);
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

        public void RemoveDrugFromFavorites(long profileId, long drugId)
        {
            long listId = GetListId(ListType.FavList, profileId);
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

        public void AddDrugToDrugList(long profileId, long drugId)
        {
            long listId = GetListId(ListType.DrugList, profileId);
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

        public void RemoveDrugFromDrugList(long profileId, long drugId)
        {
            long listId = GetListId(ListType.DrugList, profileId);
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

        #endregion

        #region Private Methods

        enum ListType
        {
            Undefined = 0,
            DrugList = 1,
            FavList = 2
        };

        private long GetProfileId(long userId)
        {
            long retVal = 0;

            string SQL = " SELECT TOP 1 * FROM [dbo].[UserProfiles] WHERE UserID = @UserId";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = (long)reader["Id"];
                        }
                    }
                }
            }

            return retVal;
        }

        private long CreateNewList(ListType type, long profileId)
        {
            long retVal = 0;
            string name = "";
            switch(type)
            {
                case ListType.DrugList:
                    name = "My Drugs";
                    break;
                case ListType.FavList:
                    name = "Fav Drugs";
                    break;
            }

            if (!string.IsNullOrEmpty(name))
            {
                string SQL = "INSERT INTO [dbo].[UserLists]( [UserProfileID],[Name],[DateCreated],[DateModified],[DataType],[System]) " +
                             "VALUES( @ProfileId,@Name,@Date,@Date,drug,1); " +
                             "SELECT Scope_Identity()";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(SQL, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProfileId", profileId);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 500));
                        cmd.Parameters["@Name"].Value = name;
                        connection.Open();
                        retVal = (long)cmd.ExecuteScalar();
                    }
                }
            }

            return retVal;
        }

        private long GetListId(ListType type, long profileId)
        {
            long retVal = 0;
            string name = "";
            switch(type)
            {
                case ListType.DrugList:
                    name = "My Drugs";
                    break;
                case ListType.FavList:
                    name = "Fav Drugs";
                    break;
            }

            if (!string.IsNullOrEmpty(name))
            {
                string SQL = "SELECT TOP 1 * FROM [dbo].[UserLists] WHERE UserProfileID = @ProfileId AND Name = @Name";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(SQL, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProfileId", profileId);
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 500));
                        cmd.Parameters["@Name"].Value = name;
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                retVal = (long)reader["ID"];
                            }
                        }
                    }
                }
            }

            return retVal;
        }

        private List<DrugListItem> GetListDrugs(long listId, long profileId)
        {
            List<DrugListItem> retList = new List<DrugListItem>();

            string SQL = @"SELECT D.[ID] AS [DrugID], D.[GenericName] AS [DrugName], L.[DateAdded], [DrugEocCounts].[NumEocs] AS [DrugEocs], [UserEocCounts].[NumEocs] AS [UserEocs]
                         FROM [dbo].[UserListItems] L
                         LEFT JOIN [dbo].[Drugs] D 
			                ON L.[ItemID] = D.[ID] 
                         LEFT JOIN (SELECT [DrugEocs].[DrugID], COUNT(1) AS [NumEocs] FROM [DrugEocs] 
						            INNER JOIN [EocUserTypes]
							            ON [DrugEocs].[EocID] = [EocUserTypes].[EocID] 
						            LEFT JOIN [UserTypes]
							            ON [EocUserTypes].[UserTypeID] = [UserTypes].[ID]
					                WHERE [UserTypes].[Name] = 'prescriber' 
					                GROUP BY [DrugEocs].[DrugID]
                                    ) AS [DrugEocCounts]
			                ON D.[ID] = [DrugEocCounts].[DrugID]
                         LEFT JOIN (SELECT [UserEocs].[ProfileID], [UserEocs].[DrugID], COUNT(1) [NumEocs] FROM [UserEocs]
					                WHERE [UserEocs].[DateCompleted] IS NOT NULL
					                GROUP BY [UserEocs].[ProfileID], [UserEocs].[DrugID]
                                    ) AS [UserEocCounts]
			                ON [UserEocCounts].[ProfileID] = @ProfileId AND D.[ID] = [UserEocCounts].[DrugID]
                         WHERE L.ListID = @ListId";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@ProfileId", profileId);
                    cmd.Parameters.AddWithValue("@ListId", listId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            retList.Add(new DrugListItem
                                        {
                                            Id = reader["DrugID"] == DBNull.Value ? 0 : (long)reader["DrugID"],
                                            DrugName = reader["DrugName"] == DBNull.Value ? string.Empty : reader["DrugName"].ToString(),
                                            DrugEocsCount = reader["DrugEocs"] == DBNull.Value ? 0 : (int)reader["DrugEocs"],
                                            UserEocsCount = reader["UserEocs"] == DBNull.Value ? 0 : (int)reader["UserEocs"],
                                            DateAdded = reader["DateAdded"] == DBNull.Value ? DateTime.Now : (DateTime)reader["DateAdded"]
                                        });
                        }
                    }
                }
            }

            return retList;
        }

        #endregion
    }
}
