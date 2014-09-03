using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint26
{
    [Migration(201409020103)]
    public class Migration0001AddClassToProviderUser : Migration
    {
        public override void Up()
        {
            Alter.Table("ProviderUsers")
                .AddColumn("Class").AsString(16).WithDefaultValue("standard");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
