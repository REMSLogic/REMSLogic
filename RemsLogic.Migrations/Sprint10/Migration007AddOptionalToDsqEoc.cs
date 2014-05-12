using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405121034)]
    public class Migration007AddOptionalToDsqEoc : Migration
    {
        public override void Up()
        {
            Alter.Table("DSQ_Eocs")
                .AddColumn("IsRequired").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
