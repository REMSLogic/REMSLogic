using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint10
{
    [Migration(201405050051)]
    public class Migration001AddRequiredColumnToDsqlLinks : Migration
    {
        public override void Up()
        {
            Alter.Table("DSQ_Links")
                .AddColumn("IsRequired").AsBoolean().WithDefaultValue(false).NotNullable()
                .AddColumn("EocId").AsInt64().WithDefaultValue(0).NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
