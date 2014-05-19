using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugSelections", PrimaryKeyColumn = "ID" )]
    public class DrugSelection : ActiveRecord
    {
        #region Persisted Member Variables
        [Column]
        public long DrugID;

        [Column]
        public long PrescriberID;

        [Column]
        public bool Prescribes;

        [Column]
        public DateTime DateRecorded;
        #endregion

        #region Member Variables
        private Prescriber _cachePrescriber;
        private Drug _cacheDrug;
        #endregion

        #region Navigation Properties
        public Prescriber Prescriber
        {
            get{return _cachePrescriber ?? (_cachePrescriber = new Prescriber(PrescriberID));}
        }

        public Drug Drug
        {
            get{return _cacheDrug ?? (_cacheDrug = new Drug(DrugID));}
        }
        #endregion

        #region Constructors
        public DrugSelection(long? id = null)
            : base(id)
        {
        }

        public DrugSelection(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Shared Methods
        public static DrugSelection Find(long prescriberId, long drugId)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * ");
            sql.Append("FROM DrugSelections ");
            sql.Append("WHERE ");
            sql.Append("    PrescriberID = "+db.DelimParameter("PrescriberId")+" AND ");
            sql.Append("    DrugID = "+db.DelimParameter("DrugId")+"; ");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("PrescriberId", prescriberId));
            ps.Add(new Parameter("DrugId", drugId));

            var rows = db.ExecuteQuery<DrugSelection>(sql.ToString(), ps.ToArray());

            if( rows == null || rows.Count <= 0 )
                return null;

            return rows[0];
        }

        public static IList<Drug> GetDrugsWithNoSelection(Prescriber prescriber)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * ");
            sql.Append("FROM "+db.DelimTable("Drugs")+" ");
            sql.Append("WHERE "+db.DelimTable("Drugs")+"."+db.DelimColumn("ID")+" NOT IN ");
            sql.Append("    (SELECT "+db.DelimColumn("DrugID")+" ");
            sql.Append("     FROM "+db.DelimTable("DrugSelections")+" ");
            sql.Append("     WHERE "+db.DelimTable("DrugSelections")+"."+db.DelimColumn("PrescriberID")+" = "+db.DelimParameter("PrescriberId")+") AND ");
            sql.Append("    "+db.DelimTable("Drugs")+"."+db.DelimColumn("Active")+" = 1;");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("PrescriberId", prescriber.ID));

            return db.ExecuteQuery<Drug>(sql.ToString(), ps.ToArray());
        }

        public static IList<Data.Drug> GetDrugsWithUpdates(Data.Prescriber prescriber)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT Drugs.* ");
            sql.Append("FROM DrugSelections ");
            sql.Append("    INNER JOIN Drugs ON DrugSelections.DrugId = Drugs.ID ");
            sql.Append("WHERE ");
            sql.Append("    PrescriberID = "+db.DelimParameter("PrescriberId")+" AND ");
            sql.Append("    Drugs.Updated > DrugSelections.DateRecorded; ");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("PrescriberId", prescriber.ID));

            return db.ExecuteQuery<Drug>(sql.ToString(), ps.ToArray());
        }

        public static bool HasDrugsWithNoSelection(Prescriber prescriber)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT COUNT(*) ");
            sql.Append("FROM "+db.DelimTable("Drugs")+" ");
            sql.Append("WHERE "+db.DelimTable("Drugs")+"."+db.DelimColumn("ID")+" NOT IN ");
            sql.Append("    (SELECT "+db.DelimColumn("DrugID")+" ");
            sql.Append("     FROM "+db.DelimTable("DrugSelections")+" ");
            sql.Append("     WHERE "+db.DelimTable("DrugSelections")+"."+db.DelimColumn("PrescriberID")+" = "+db.DelimParameter("PrescriberId")+") AND ");
            sql.Append("    "+db.DelimTable("Drugs")+"."+db.DelimColumn("Active")+" = 1;");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("PrescriberId", prescriber.ID));

            return (db.ExecuteScalar<int>(sql.ToString(), ps.ToArray()) > 0);
        }

        public static bool HasDrugsWithUpdates(Data.Prescriber prescriber)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT COUNT(*) ");
            sql.Append("FROM DrugSelections ");
            sql.Append("    INNER JOIN Drugs ON DrugSelections.DrugId = Drugs.ID ");
            sql.Append("WHERE ");
            sql.Append("    PrescriberID = "+db.DelimParameter("PrescriberId")+" AND ");
            sql.Append("    Drugs.Updated > DrugSelections.DateRecorded AND ");
            sql.Append("    Drugs.Active = 1 ");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("PrescriberId", prescriber.ID));

            return ( db.ExecuteScalar<int>(sql.ToString(), ps.ToArray()) > 0);
        }
        #endregion
    }
}
