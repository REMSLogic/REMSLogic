using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405120347)]
    public class Migration006AddDeletedToOrganization : Migration
    {
        public override void Up()
        {
            Alter.Table("Organizations")
                .AddColumn("Deleted").AsBoolean().WithDefaultValue(false).NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
