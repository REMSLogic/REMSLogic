using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint09
{
    [Migration(201404240519)]
    public class Migration002AddSetIdentityAttribute : Migration
    {
        public override void Up()
        {
            Delete.Table("UserEocsLog");

            Create.Table("UserEocsLog")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserEocsId").AsInt64().NotNullable()
                .WithColumn("RecordedAt").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
