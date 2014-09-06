using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint26
{
    [Migration(201409020103)]
    public class Migration0001AddIsEcommToUserProfile : Migration
    {
        public override void Up()
        {
            Alter.Table("UserProfiles")
                .AddColumn("IsEcommerce").AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
