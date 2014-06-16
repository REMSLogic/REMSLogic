using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint14
{
    [Migration(201406150912)]
    public class Migration001AddIsEnabledToSection : Migration
    {
        public override void Up()
        {
            Create.Table("DSQ_SectionSettings")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("SectionId").AsInt64().NotNullable()
                .WithColumn("DrugId").AsInt64().NotNullable()
                .WithColumn("IsEnabled").AsBoolean().NotNullable().WithDefaultValue(true);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
