using FluentMigrator;

namespace RemsLogic.Migrations.Sprint11
{
    [Migration(201405150102)]
    public class Migration001AddPrereqToEocLink : Migration
    {
        public override void Up()
        {
            Alter.Table("DSQ_Links")
                .AddColumn("HasPrereq").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}
