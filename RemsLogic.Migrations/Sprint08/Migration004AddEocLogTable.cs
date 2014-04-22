using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint08
{
    [Migration(201404211202)]
    public class Migration004AddEocLogTable : Migration
    {
        public override void Up()
        {
            Create.Table("UserEocsLog")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("UserEocsId").AsInt64().NotNullable()
                .WithColumn("RecordedAt").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
