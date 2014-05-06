using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405051011)]
    public class Migration002AddOrganizationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Organizations")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("PrimaryFacilityId").AsInt64().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
