using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint15
{
    [Migration(201406300134)]
    public class Migration0002AddDisplayForToEocs : Migration
    {
        public override void Up()
        {
            Alter.Table("Eocs")
                .AddColumn("DisplayForRoles").AsString(256).WithDefaultValue(String.Empty);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
