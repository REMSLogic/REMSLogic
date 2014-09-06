using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint26
{
    [Migration(201409020103)]
    public class Migration0002AddAccountTable : Migration
    {
        public override void Up()
        {
            Create.Table("Accounts")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserProfileId").AsInt64().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("ExpiresOn").AsDateTime().NotNullable()
                .WithColumn("Enabled").AsBoolean().NotNullable().WithDefaultValue(true);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
