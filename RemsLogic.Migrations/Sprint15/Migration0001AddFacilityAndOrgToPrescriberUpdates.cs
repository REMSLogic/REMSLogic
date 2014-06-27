using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint15
{
    [Migration(201406270440)]
    public class Migration0001AddFacilityAndOrgToPrescriberUpdates : Migration
    {
        public override void Up()
        {
            Alter.Table("PrescriberUpdates")
                .AddColumn("FacilityId").AsInt64().NotNullable().WithDefaultValue(0)
                .AddColumn("OrganizationId").AsInt64().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
