using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint07
{
    [Migration(201404110425)]
    public class Migration002AddWidgetSettings : Migration
    {
        public override void Up()
        {
            Create.Table("UserWidgetSettings")
                .WithColumn("Id").AsInt64()
                    .PrimaryKey()
                    .Identity()
                    .NotNullable()
                .WithColumn("UserId").AsInt64()
                    .NotNullable()
                .WithColumn("Column1").AsString()
                    .NotNullable()
                .WithColumn("Column2").AsString()
                    .NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
