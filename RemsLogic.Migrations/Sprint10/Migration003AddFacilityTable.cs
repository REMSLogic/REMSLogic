using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405051041)]
    public class Migration003AddFacilityTable : Migration
    {
        public override void Up()
        {
            Create.Table("Facilities")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AddressId").AsInt64().NotNullable()
                .WithColumn("OrganizationId").AsInt64().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("BedSize").AsString().NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
