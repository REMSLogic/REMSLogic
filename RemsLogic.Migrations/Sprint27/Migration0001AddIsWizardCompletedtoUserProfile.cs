using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint27
{
    [Migration(201410271345)]
    public class Migration0001AddIsWizardCompletedtoUserProfile : Migration
    {
        public override void Up()
        {
            Alter.Table("UserProfiles")
                .AddColumn("IsWizardComplete").AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
