using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint08
{
    [Migration(201404210348)]
    public class Migration001AddDeletedColumnToUserEoc : Migration
    {
        public override void Up()
        {
            Alter.Table("UserEocs")
                .AddColumn("Deleted").AsBoolean()
                    .NotNullable()
                    .WithDefaultValue(false);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
