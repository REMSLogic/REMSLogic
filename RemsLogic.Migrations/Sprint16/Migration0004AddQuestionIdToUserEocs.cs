using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint16
{
    [Migration(201407201026)]
    public class Migration0004AddQuestionIdToUserEocs : Migration
    {
        public override void Up()
        {
            Alter.Table("UserEocs")
                .AddColumn("QuestionId").AsInt64().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
