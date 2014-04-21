using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint08
{
    [Migration(201404210546)]
    public class Migration002MakeDateCompletedNullable : Migration
    {
        public override void Up()
        {
            Alter.Table("UserEocs")
                .AlterColumn("DateCompleted").AsDateTime()
                    .Nullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
