using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405120333)]
    public class Migration005AddOrganizationIdToPrescriberProfile : Migration
    {
        public override void Up()
        {
            Alter.Table("PrescriberProfile")
                .AddColumn("OrganizationId").AsInt64().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
