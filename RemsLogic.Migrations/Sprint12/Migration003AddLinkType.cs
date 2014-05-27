using FluentMigrator;

namespace RemsLogic.Migrations.Sprint12
{
    [Migration(201405270445)]
    public class Migration003AddLinkType : Migration
    {
        public override void Up()
        {
            Alter.Table("DSQ_Links")
                .AddColumn("LinkType").AsString(32).NotNullable().WithDefaultValue("N/A");
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}
