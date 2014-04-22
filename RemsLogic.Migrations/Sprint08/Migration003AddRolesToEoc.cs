using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint08
{
    [Migration(201404211004)]
    public class Migration003AddRolesToEoc : Migration
    {
        public override void Up()
        {
            Alter.Table("Eocs")
                .AddColumn("Roles").AsString(256)
                    .Nullable();

            Update.Table("Eocs")
                .Set(new {Roles = "view_prescriber"})
                .AllRows();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
