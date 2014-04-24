using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint09
{
    [Migration(201404240403)]
    public class Migration001AddEocToDsqQuestion : Migration
    {
        public override void Up()
        {
            Alter.Table("DSQ_Questions")
                .AddColumn("EocId").AsInt64().Nullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
