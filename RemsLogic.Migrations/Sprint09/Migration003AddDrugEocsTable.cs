using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint09
{
    [Migration(201404280448)]
    public class Migration003AddDrugEocsTable : Migration
    {
        public override void Up()
        {
            Create.Table("DSQ_Eocs")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("DrugId").AsInt64().NotNullable()
                .WithColumn("EocId").AsInt64().NotNullable()
                .WithColumn("QuestionId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
