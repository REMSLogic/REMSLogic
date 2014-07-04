using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint16
{
    [Migration(201407040212)]
    class Migration0001AddRestrictedLinkTable : Migration
    {
        public override void Up()
        {
            Create.Table("RestrictedLinks")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Url").AsString(512).NotNullable()
                .WithColumn("Token").AsGuid().NotNullable()
                .WithColumn("ExpirationDate").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
