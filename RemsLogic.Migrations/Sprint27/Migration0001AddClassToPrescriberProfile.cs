using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint27
{
    [Migration(201409061258)]
    public class Migration0001AddClassToPrescriberProfile : Migration
    {
        public override void Up()
        {
            Alter.Table("PrescriberProfiles")
                .AddColumn("Class").AsString(16).WithDefaultValue("standard");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
