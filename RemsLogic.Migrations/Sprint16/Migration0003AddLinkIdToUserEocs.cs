using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint16
{
    [Migration(201407201025)]
    public class Migration0003AddLinkIdToUserEocs : Migration
    {
        public override void Up()
        {
            Alter.Table("UserEocs")
                .AddColumn("LinkId").AsInt64().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
