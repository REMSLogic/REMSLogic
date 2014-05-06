using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405051944)]
    public class Migration004AddOrganizationIdToProviderUser : Migration
    {
        public override void Up()
        {
            Alter.Table("ProviderUsers")
                .AddColumn("OrganizationId").AsInt64().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
