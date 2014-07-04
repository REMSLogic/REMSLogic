using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint16
{
    [Migration(201407040309)]
    public class Migration0002AddCreateForColumnToRestrictedLinks : Migration
    {
        public override void Up()
        {
            Alter.Table("RestrictedLinks")
                .AddColumn("CreatedFor").AsString(512);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
